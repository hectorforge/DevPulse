using Domain.Enums;

namespace Application.Mappings;

public static class RoleMapExtension
{
    public static string ToFriendlyString(this Role role)
    {
        return role switch
        {
            Role.Developer => "Desarrollador",
            Role.DevOps => "Ingeniero DevOps",
            Role.QualityAssurance => "Control de Calidad",
            Role.ProjectManager => "Gerente de Proyecto",
            _ => role.ToString()
        };
    }
}