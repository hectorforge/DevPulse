using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IIncidentService _incidentService;

    public IndexModel(
        ILogger<IndexModel> logger,
        IIncidentService incidentService)
    {
        _logger = logger;
        _incidentService = incidentService;
    }

    // Propiedades
    public IEnumerable<IncidentDto> Incidents { get; set; } = new List<IncidentDto>();
    
    [BindProperty(SupportsGet = true)]
    public string? SearchName { get; set; }

    [BindProperty(SupportsGet = true)]
    public Severity? FilterSeverity { get; set; }

    [BindProperty(SupportsGet = true)]
    public int CurrentPage { get; set; } = 1;
    
    public int PageSize { get; set; } = 10;

    public async Task OnGetAsync()
    {
        _logger.LogInformation("Cargando Dashboard de Incidentes - Página: {Page}", CurrentPage);
        
        Incidents = await _incidentService.GetAllIncidentsAsync(new IncidentQueryDto(SearchName, FilterSeverity, CurrentPage, PageSize));
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        _logger.LogWarning("Solicitud de eliminación para ID: {Id}", id);
        var result = await _incidentService.DeleteIncidentAsync(id);
        
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
        }
        else
        {
            TempData["SuccessMessage"] = "Incidente eliminado correctamente.";
        }

        return RedirectToPage();
    }
    
    public string GetSeverityBadgeClass(Severity severity)
    {
        return severity switch
        {
            Severity.High => "bg-danger",
            Severity.Medium => "bg-warning text-dark",
            Severity.Low => "bg-success",
            Severity.Critical => "bg-dark",
            _ => "bg-secondary"
        };
    }
}