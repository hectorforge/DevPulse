using Domain.Enums;

namespace Application.DTOs;

public record IncidentDto(
    Guid Id,
    string Title,
    string Severity,
    string Status,
    DateTime CreatedAt
);

public record CreateIncidentRequest(
    string Title,
    string Description,
    int Severity
);

public record UpdateIncidentRequest(
    Guid Id,
    string Title,
    string Description,
    Severity Severity,
    IncidentStatus Status
);