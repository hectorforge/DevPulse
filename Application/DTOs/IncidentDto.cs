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
    Guid TeamMemberId,
    string NameTeamMember
);

public record CreateIncidentRequest(
    string Title,
    string Description,
    Severity Severity
);

public record AssignIncidentRequest(
    Guid IncidentId, 
    Guid TeamMemberId
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