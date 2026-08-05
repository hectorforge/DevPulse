using Application.DTOs;
using Domain.Entities;

namespace Application.Mappings;

public static class IncidentMappingExtensions
{
    public static IncidentDto ToDto(this Incident incident)
    {
        return new IncidentDto(
            incident.Id,
            incident.Title,
            incident.Description,
            incident.Severity,
            incident.Status,
            incident.ExpectedResolutionAt,
            incident.ReportedAt,
            incident.ResolvedAt,
            incident.AssignedTo?.Id ?? Guid.Empty,
            incident.AssignedTo?.Name ?? "Sin asignar" 
        );
    }
    
    public static IEnumerable<IncidentDto> ToDtoList(this IEnumerable<Incident> incidents)
    {
        return incidents.Select(incident => incident.ToDto());
    }
}