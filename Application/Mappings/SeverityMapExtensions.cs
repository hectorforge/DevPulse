using Domain.Enums;

namespace Application.Mappings;

public static class SeverityMapExtensions
{
    public static string ToFriendlyString(this Severity severity)
    {
        return severity switch
        {
            Severity.Low => "Baja",
            Severity.Medium => "Media",
            Severity.High => "Alta",
            Severity.Critical => "Crítica",
            _ => severity.ToString()
        };
    }
}