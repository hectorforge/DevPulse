using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Web.ViewModels.TeamMembers;

public class TeamMemberFormViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [Display(Name = "Nombre Completo")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Correo electrónico no válido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Debe seleccionar un rol")]
    public Role Role { get; set; }

    public bool IsEdit => Id.HasValue && Id.Value != Guid.Empty;
}