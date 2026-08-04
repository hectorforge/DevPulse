using Application.DTOs;
using Application.Interfaces;
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

    public IndexModel(IIncidentService incidentService, ITeamMemberRepository teamMemberRepository)
    {
        _incidentService = incidentService;
        _teamMemberRepository = teamMemberRepository;
    }

    [BindProperty(SupportsGet = true)] public string? SearchName { get; set; }
    [BindProperty(SupportsGet = true)] public Severity? FilterSeverity { get; set; }
    [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
    public int TotalRecords { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    
    public IEnumerable<IncidentDto> Incidents { get; set; } = new List<IncidentDto>();
    public IEnumerable<SelectListItem> SeverityOptions { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> TeamMemberOptions { get; set; } = new List<SelectListItem>();

    public async Task OnGetAsync()
    {
        ChargeSeverityOptions();
        var query = new IncidentQueryDto(SearchName, FilterSeverity, CurrentPage, PageSize);
        Incidents = await _incidentService.GetAllIncidentsAsync(query);
        TotalRecords = Incidents.Count(); 
    }

    private void ChargeSeverityOptions()
    {
        SeverityOptions = Enum.GetValues<Severity>().Select(s => new SelectListItem 
        {
            Value = s.ToString(),
            Text = s.ToString()
        }).ToList();
    }

    #region Modales

    public PartialViewResult OnGetCreateModal()
    {
        var emptyRequest = new CreateIncidentRequest("", "", Severity.Low);
        return Partial("_IncidentModal", emptyRequest);
    }

    public async Task<PartialViewResult> OnGetEditModal(Guid id)
    {
        var result = await _incidentService.GetByIdAsync(id);
        var incident = result.Value;
        
        var updateRequest = new UpdateIncidentRequest(
            incident.Id,
            incident.Title,
            incident.Description,
            incident.Severity,
            incident.Status
        );
        
        return Partial("_IncidentModal", updateRequest);
    }

    public async Task<PartialViewResult> OnGetDeleteModal(Guid id)
    {
        var result = await _incidentService.GetByIdAsync(id);
        return Partial("_DeleteIncidentModal", result.Value);
    }
    
    public async Task<PartialViewResult> OnGetAssignModal(Guid id)
    {
        var result = await _incidentService.GetByIdAsync(id);
        var members = await _teamMemberRepository.GetAllAsync();

        var viewModel = new AssignMemberViewModel
        {
            IncidentTitle = result.Value.Title,
            Request = new AssignIncidentRequest(id, Guid.Empty),
            TeamMemberOptions = members.Select(m => new SelectListItem {
                Value = m.Id.ToString(),
                Text = m.Name
            }).ToList()
        };

        return Partial("_AssignMemberModal", viewModel);
    }

    #endregion

    #region Acciones (JSON Results)

    public async Task<IActionResult> OnPostCreate(CreateIncidentRequest input)
    {
        var result = await _incidentService.CreateIncidentAsync(input);
        return new JsonResult(new { success = result.IsSuccess, message = result.Message, errors = result.ErrorsValidations });
    }

    public async Task<IActionResult> OnPostEdit(UpdateIncidentRequest input)
    {
        var result = await _incidentService.UpdateIncidentAsync(input);
        return new JsonResult(new { success = result.IsSuccess, message = result.Message, errors = result.ErrorsValidations });
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        var result = await _incidentService.DeleteIncidentAsync(id);
        return new JsonResult(new { success = result.IsSuccess, message = result.Message });
    }
    
    public async Task<IActionResult> OnPostAssign(AssignIncidentRequest input)
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