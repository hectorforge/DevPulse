using Domain.Enums;

namespace Application.DTOs;

public record IncidentDto(
    Guid Id,
    string Title,
    string Description,
    Severity Severity,
    IncidentStatus Status,
    DateTime ExpectedResolutionAt,
    DateTime ReportedAt,
    DateTime? ResolvedAt,
    string NameTeamMember
);

public record CreateIncidentRequest(
    string Title,
    string Description,
    Severity Severity
);

public record UpdateIncidentRequest(
    Guid Id,
    string Title,
    string Description,
    Severity Severity,
    IncidentStatus Status
);

public record IncidentQueryDto(
    string? name,
    Severity? severity, 
    int page,
    int size);