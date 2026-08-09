using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class PostMortemRepository : IPostMortemRepository
{
    private readonly AppDbContext _context;

    public PostMortemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<PostMortem> Items, int TotalCount)> GetPagedAsync(
        string? name, 
        int page, 
        int size, 
        CancellationToken ct = default)
    {
        IQueryable<PostMortem> query = _context.PostMortems;
        
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(pm => EF.Functions.ILike(pm.RootCause, $"%{name}%"));
        }

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Include(pm => pm.Incident)
            .AsNoTracking()
            .OrderByDescending(pm => pm.AuditRecord.CreatedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);

        return (items, totalCount);
    }

    public async Task<PostMortem?> GetByIdAsync(
        Guid id, 
        bool track = true, 
        CancellationToken ct = default)
    {
        IQueryable<PostMortem> query = _context.PostMortems;
        
        if (!track)
        {
            query = query.AsNoTracking();
        }

        return await query
            .Include(pm => pm.Incident)
            .FirstOrDefaultAsync(pm => pm.Id == id, ct);
    }

    public async Task AddAsync(PostMortem postMortem, CancellationToken ct = default)
    {
        await _context.PostMortems.AddAsync(postMortem, ct);
    }

    public void Update(PostMortem postMortem)
    {
        _context.PostMortems.Update(postMortem);
    }

    public void Delete(PostMortem postMortem)
    {
        _context.PostMortems.Remove(postMortem);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.PostMortems.AnyAsync(pm => pm.Id == id, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}