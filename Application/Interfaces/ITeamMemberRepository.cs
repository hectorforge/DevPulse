using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces;

public interface ITeamMemberRepository
{
    Task<(IEnumerable<TeamMember> Items, int TotalCount)> GetPagedAsync(string? name, Role? rol, int page, int size, CancellationToken ct = default);
    Task<TeamMember?> GetByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task<TeamMember?> GetByEmailAsync(string email, CancellationToken ct = default);
    Task AddAsync(TeamMember member, CancellationToken ct = default);
    void Update(TeamMember member);
    void Delete(TeamMember member);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}