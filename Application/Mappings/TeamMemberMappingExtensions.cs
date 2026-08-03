using Application.DTOs;
using Domain.Entities;

namespace Application.Mappings;

public static class TeamMemberMappingExtensions
{
    public static TeamMemberDto toDto(this TeamMember teamMember)
    {
        return new TeamMemberDto(
            teamMember.Id, 
            teamMember.Name, 
            teamMember.Email, 
            teamMember.Role.ToString());
    }

    public static IEnumerable<TeamMemberDto> toDto(this IEnumerable<TeamMember> teamMembers)
    {
        return teamMembers.Select(x => x.toDto());
    }
}