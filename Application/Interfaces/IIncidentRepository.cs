using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces;

public interface IIncidentRepository
{
    Task<(IEnumerable<Incident> Items, int TotalCount)> GetPagedAsync(string? name, Severity? severity, int page, int size, CancellationToken ct = default);
    Task<Incident?> GetByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task AddAsync(Incident incident, CancellationToken ct = default);
    void Update(Incident incident);
    void Delete(Incident incident);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}