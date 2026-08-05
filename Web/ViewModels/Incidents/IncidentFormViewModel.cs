using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Web.ViewModels.Incidents;

public class IncidentFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio")]
    [Display(Name = "Título del Incidente")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es obligatoria")]
    [Display(Name = "Descripción Detallada")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe seleccionar una severidad")]
    public Severity Severity { get; set; }

    public IncidentStatus Status { get; set; }
    
    
    public bool IsEdit => Id.HasValue && Id.Value != Guid.Empty;
}