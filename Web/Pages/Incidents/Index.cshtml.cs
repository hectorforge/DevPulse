using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using Application.Services.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ViewModels.Incidents;

namespace Web.Pages.Incidents;

public class IndexModel : PageModel
{
    private readonly IIncidentService _incidentService;
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IFileStorageService _storageService;

    public IndexModel(
        IIncidentService incidentService, 
        ITeamMemberRepository teamMemberRepository, 
        IFileStorageService storageService)
    {
        _incidentService = incidentService;
        _teamMemberRepository = teamMemberRepository;
        _storageService = storageService;
    }

    [BindProperty(SupportsGet = true)] public string? SearchName { get; set; }
    [BindProperty(SupportsGet = true)] public Severity? FilterSeverity { get; set; }
    [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
    public int TotalRecords { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    
    public bool IsPartial { get; set; }
    
    public IEnumerable<IncidentDto> Incidents { get; set; } = new List<IncidentDto>();
    public IEnumerable<SelectListItem> SeverityOptions { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> TeamMemberOptions { get; set; } = new List<SelectListItem>();

    public async Task<IActionResult> OnGetAsync(bool isPartial = false)
    {
        ChargeSeverityOptions();
        var query = new IncidentQueryDto(SearchName, FilterSeverity, CurrentPage, PageSize);
        var pagedResult = await _incidentService.GetAllIncidentsAsync(query); 
        
        Incidents = pagedResult.Items; 
        TotalRecords = pagedResult.TotalRecords;

        if (isPartial)
        {
            return Partial("_IncidentList", this);
        }
        return Page();
    }

    private void ChargeSeverityOptions()
    {
        SeverityOptions = Enum.GetValues<Severity>().Select(s => new SelectListItem 
        {
            Value = s.ToString(),
            Text = s.ToFriendlyString()
        }).ToList();
    }

    #region Modales
    public async Task<PartialViewResult> OnGetScreenshotModal(Guid id)
    {
        var result = await _incidentService.GetByIdAsync(id);
        var incident = result.Value;
        return Partial("_IncidentScreenshotModal", incident);
    }
    
    public PartialViewResult OnGetCreateModal()
    {
        var viewModel = new IncidentFormViewModel(); 
        return Partial("_IncidentModal", viewModel);
    }

    public async Task<PartialViewResult> OnGetEditModal(Guid id)
    {
        var result = await _incidentService.GetByIdAsync(id);
        var incident = result.Value;
        
        var viewModel = new IncidentFormViewModel
        {
            Id = incident.Id,
            Title = incident.Title,
            Description = incident.Description,
            Severity = incident.Severity,
            Status = incident.Status,
            ScreenshotUrl = incident.ScreenshotUrl,
            Recomendation = incident.Recommendation
        };
    
        return Partial("_IncidentModal", viewModel);
    }

    public async Task<PartialViewResult> OnGetDeleteModal(Guid id)
    {
        var result = await _incidentService.GetByIdAsync(id);
        return Partial("_DeleteIncidentModal", result.Value);
    }
    
    public async Task<PartialViewResult> OnGetAssignModal(Guid id)
    {
        var result = await _incidentService.GetByIdAsync(id);
        var incident = result.Value;
        
        var currentMemberId = incident?.TeamMemberId ?? Guid.Empty;
        
        var members = await _teamMemberRepository.GetAllAsync();

        var viewModel = new AssignMemberViewModel
        {
            IncidentTitle = incident?.Title ?? "Incidente Desconocido",
            Request = new AssignIncidentRequest(id, currentMemberId),
            TeamMemberOptions = members.Select(m => new SelectListItem {
                Value = m.Id.ToString(),
                Text = m.Name,
                Selected = m.Id == currentMemberId
            }).ToList()
        };

        return Partial("_AssignMemberModal", viewModel);
    }

    #endregion

    #region Acciones (JSON Results)
    public async Task<IActionResult> OnPostCreate(IncidentFormViewModel viewModel)
    {
        string imageUrl = "";
        
        if (viewModel.ScreenshotFile != null)
        {
            imageUrl = await _storageService.UploadImageAsync(viewModel.ScreenshotFile, "incidents");
        }
        
        var request = new CreateIncidentRequest(
            viewModel.Title,
            viewModel.Description,
            viewModel.Severity,
            imageUrl,
            viewModel.Recomendation
        );

        var result = await _incidentService.CreateIncidentAsync(request);
        return new JsonResult(new { success = result.IsSuccess, message = result.Message, errors = result.ErrorsValidations });
    }
    
    public async Task<IActionResult> OnPostEdit(IncidentFormViewModel viewModel)
    {
        string imageUrl = viewModel.ScreenshotUrl ?? "";
        
        if (viewModel.ScreenshotFile != null)
        {
            imageUrl = await _storageService.UploadImageAsync(viewModel.ScreenshotFile, "incidents");
        }

        var request = new UpdateIncidentRequest(
            viewModel.Id.Value,
            viewModel.Title,
            viewModel.Description,
            viewModel.Severity,
            viewModel.Status,
            imageUrl,
            viewModel.Recomendation
        );

        var result = await _incidentService.UpdateIncidentAsync(request);
        return new JsonResult(new { success = result.IsSuccess, message = result.Message, errors = result.ErrorsValidations });
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        var result = await _incidentService.DeleteIncidentAsync(id);
        return new JsonResult(new { success = result.IsSuccess, message = result.Message });
    }
    
    public async Task<IActionResult> OnPostAssign([Bind(Prefix = "Request")]AssignIncidentRequest input)
    {
        var result = await _incidentService.AssignTeamMemberAsync(input);
        return new JsonResult(new { 
            success = result.IsSuccess, 
            message = result.Message, 
            errors = result.ErrorsValidations 
        });
    }
    #endregion
}