using Domain.Entities;

namespace Application.Interfaces;

public interface IPostMortemRepository
{
    Task<(IEnumerable<PostMortem> Items, int TotalCount)> GetPagedAsync(string? name,int page, int size ,CancellationToken ct = default);
    
    Task<PostMortem?> GetByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task AddAsync(PostMortem postMortem, CancellationToken ct = default);
    void Update(PostMortem postMortem);
    void Delete(PostMortem postMortem);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}