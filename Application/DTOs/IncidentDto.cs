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
    string NameTeamMember,
    string ScreenshotUrl,
    string Recommendation
);

public record CreateIncidentRequest(
    string Title,
    string Description,
    Severity Severity,
    string ScreenshotUrl,
    string Recommendation
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
    IncidentStatus Status,
    string ScreenshotUrl,
    string Recommendation
);

public record IncidentQueryDto(
    string? name,
    Severity? severity, 
    int page,
    int size);
    
public record IncidentSelectDto(
    Guid Id,
    string RootCause
);