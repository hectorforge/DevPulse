using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ViewModels.PostMortem;

namespace Web.Pages.PostMortems;

public class IndexModel : PageModel
{
    private readonly IPostMortemService _postMortemService;
    private readonly IIncidentService _incidentService;

    public IndexModel(IPostMortemService postMortemService, IIncidentService incidentService)
    {
        _postMortemService = postMortemService;
        _incidentService = incidentService;
    }

    [BindProperty(SupportsGet = true)] public string? SearchRootCause { get; set; }
    [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;
    public int TotalRecords { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    
    public IEnumerable<PostMortemDto> PostMortems { get; set; } = new List<PostMortemDto>();

    public async Task<IActionResult> OnGetAsync(bool isPartial = false)
    {
        var (items, total) = await _postMortemService.GetAll(SearchRootCause ?? "", CurrentPage, PageSize);
        
        PostMortems = items;
        TotalRecords = total;

        if (isPartial) return Partial("_PostMortemList", this);
        return Page();
    }

    #region Modales

    public async Task<PartialViewResult> OnGetCreateModal()
    {
        var incidents = await _incidentService.SearchForSelect(null, 20);
        
        var viewModel = new PostMortemFormViewModel
        {
            IncidentOptions = incidents.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.RootCause
            }).ToList()
        };

        return Partial("_PostMortemModal", viewModel);
    }

    public async Task<PartialViewResult> OnGetEditModal(Guid id)
    {
        var result = await _postMortemService.GetById(id);
        var pm = result.Value;
    
        var incidentResult = await _incidentService.GetByIdAsync(pm.IncidentId);
        ViewData["CurrentIncidentTitle"] = incidentResult.Value?.Title ?? "Incidente no encontrado";

        var viewModel = new PostMortemFormViewModel {
            Id = pm.Id,
            RootCause = pm.RootCause,
            LessonsLearned = pm.LessonsLearned,
            IncidentId = pm.IncidentId,
            IncidentOptions = new List<SelectListItem>() 
        };

        return Partial("_PostMortemModal", viewModel);
    }
    
    
    public async Task<JsonResult> OnGetSearchIncidents(string term)
    {
        var results = await _incidentService.SearchForSelect(term);
        return new JsonResult(results);
    }
    
    public async Task<PartialViewResult> OnGetDeleteModal(Guid id)
    {
        var result = await _postMortemService.GetById(id);
        return Partial("_DeletePostMortemModal", result.Value);
    }

    #endregion

    #region Acciones

    public async Task<IActionResult> OnPostCreate(PostMortemFormViewModel viewModel)
    {
        var request = new CreatePostDto(viewModel.RootCause, viewModel.LessonsLearned, viewModel.IncidentId);
        var result = await _postMortemService.Add(request);
        return new JsonResult(new { success = result.IsSuccess, message = result.Message, errors = result.ErrorsValidations });
    }

    public async Task<IActionResult> OnPostEdit(PostMortemFormViewModel viewModel)
    {
        var request = new UpdatePostDto(viewModel.Id.Value, viewModel.RootCause, viewModel.LessonsLearned);
        var result = await _postMortemService.Update(request);
        return new JsonResult(new { success = result.IsSuccess, message = result.Message, errors = result.ErrorsValidations });
    }

    public async Task<IActionResult> OnPostDelete(Guid id)
    {
        var result = await _postMortemService.Delete(id);
        return new JsonResult(new { success = result.IsSuccess, message = result.Message });
    }

    #endregion
}