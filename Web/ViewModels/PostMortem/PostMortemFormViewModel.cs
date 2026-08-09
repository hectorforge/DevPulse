using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModels.PostMortem;

public class PostMortemFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "La causa raíz es obligatoria")]
    [Display(Name = "Causa Raíz")]
    public string RootCause { get; set; } = string.Empty;

    [Required(ErrorMessage = "Las lecciones aprendidas son obligatorias")]
    [Display(Name = "Lecciones Aprendidas")]
    public string LessonsLearned { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe seleccionar un incidente")]
    [Display(Name = "Incidente Relacionado")]
    public Guid IncidentId { get; set; }

    public bool IsEdit => Id.HasValue;
    
    public IEnumerable<SelectListItem>? IncidentOptions { get; set; }
}