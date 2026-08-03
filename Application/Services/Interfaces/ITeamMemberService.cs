using Application.DTOs;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services.Interfaces;

public interface ITeamMemberService
{
    Task<(ICollection<TeamMemberDto> Items, int TotalRecords)> GetAll(string? name , Role? role, int page = 1, int size=10);
    Task<Result<TeamMemberDto>> Add(CreateTeamMemberDto dto);
    Task<Result<TeamMemberDto?>> Update(UpdateTeamMemberDto dto);
    Task<Result<TeamMemberDto?>> Delete(Guid id);
    Task<Result<TeamMemberDto?>> GetById(Guid id);
}