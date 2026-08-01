using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly AppDbContext _context;

    public IncidentRepository(AppDbContext context) => _context = context;

    public async Task<(IEnumerable<Incident> Items, int TotalCount)> GetPagedAsync(
        string? name, 
        Severity? severity, 
        int page, 
        int size,
        CancellationToken ct = default)
    {
        IQueryable<Incident> query = _context.Incidents;
        
        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(i => EF.Functions.ILike(i.Title, $"%{name}%"));
        }

        if (severity.HasValue)
        {
            query = query.Where(i => i.Severity == severity.Value);
        }
        
        var totalCount = await query.CountAsync(ct);
        
        var items = await query
            .AsNoTracking()
            .OrderByDescending(i => i.CreatedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(ct);

        return (items, totalCount);
    }

    public async Task<Incident?> GetByIdAsync(
        Guid id, 
        bool track = true, 
        CancellationToken ct = default)
    {
        IQueryable<Incident> query = _context.Incidents;

        // [PRO] Eager Loading condicional: Si necesitas datos relacionados (ej: PostMortem)
        // query = query.Include(i => i.PostMortem); 

        if (!track)
        {
            query = query.AsNoTracking();
        }

        // [PRO] FirstOrDefaultAsync es más rápido que FindAsync cuando usas IQueryable/AsNoTracking
        return await query.FirstOrDefaultAsync(i => i.Id == id, ct);
    }

    public async Task AddAsync(Incident incident, CancellationToken ct = default)
    {
        // [PRO] AddAsync solo es necesario si usas generadores de llaves especiales. 
        // Para la mayoría de casos, _context.Add es suficiente, pero AddAsync es estándar en interfaces async.
        await _context.Incidents.AddAsync(incident, ct);
    }

    public void Update(Incident incident)
    {
        // [PRO] No uses métodos async para Update/Delete. EF solo marca el estado de la entidad en memoria.
        // El trabajo real lo hace SaveChangesAsync.
        _context.Incidents.Update(incident);
    }

    public void Delete(Incident incident)
    {
        _context.Incidents.Remove(incident);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Incidents.AnyAsync(i => i.Id == id, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}