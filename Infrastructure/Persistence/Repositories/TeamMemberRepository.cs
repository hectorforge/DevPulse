using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly AppDbContext _context;

    public TeamMemberRepository(AppDbContext context) => _context = context;

    
    
    public async Task<(IEnumerable<TeamMember> Items, int TotalCount)> GetPagedAsync(
        string? searchTerm, 
        Role? role, 
        int page, 
        int size,
        CancellationToken ct = default)
    {
        IQueryable<TeamMember> query = _context.TeamMembers;
        
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(t => 
                EF.Functions.ILike(t.Name, $"%{searchTerm}%") || 
                EF.Functions.ILike(t.Email, $"%{searchTerm}%"));
        }
        
        if (role.HasValue)
        {
            query = query.Where(t => t.Role == role.Value);
        }
        
        var totalCount = await query.CountAsync(ct);
        
        var items = await query
            .AsNoTracking()
            .OrderBy(t => t.Name) 
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);

        return (items, totalCount);
    }

    public async Task<IEnumerable<TeamMember>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.TeamMembers.ToListAsync(ct);
    }

    public async Task<TeamMember?> GetByIdAsync(
        Guid id, 
        bool track = true, 
        CancellationToken ct = default)
    {
        IQueryable<TeamMember> query = _context.TeamMembers;

        if (!track)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<TeamMember?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _context.TeamMembers
            .FirstOrDefaultAsync(t => t.Email.ToLower() == email.ToLower().Trim(), ct);
    }

    public async Task AddAsync(TeamMember teamMember, CancellationToken ct = default)
    {
        await _context.TeamMembers.AddAsync(teamMember, ct);
    }

    public void Update(TeamMember teamMember)
    {
        _context.TeamMembers.Update(teamMember);
    }

    public void Delete(TeamMember teamMember)
    {
        _context.TeamMembers.Remove(teamMember);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.TeamMembers.AnyAsync(t => t.Id == id, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}