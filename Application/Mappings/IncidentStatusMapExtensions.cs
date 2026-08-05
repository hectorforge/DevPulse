using Domain.Enums;

namespace Application.Mappings;

public static class IncidentStatusMapExtensions
{
    public static string ToFriendlyString(this IncidentStatus status)
    {
        return status switch
        {
            IncidentStatus.Reported => "Reportado",
            IncidentStatus.Open => "Abierto",
            IncidentStatus.InProgress => "En progreso",
            IncidentStatus.Resolved => "Resuelto",
            IncidentStatus.Closed => "Cerrado",
            _ => status.ToString()
        };
    }
}