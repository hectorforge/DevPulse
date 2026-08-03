using Domain.Enums;

namespace Application.DTOs;

public record TeamMemberDto(
    Guid Id,
    string Name,
    string Email,
    string Role
);

public record CreateTeamMemberDto(
    string Name,
    string Email,
    Role Role
);

public record UpdateTeamMemberDto(
    Guid Id,
    string Name,
    string Email,
    Role Role
);