using Domain;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    
    public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    private readonly string _currentUser = "SystemUser"; 
    
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<PostMortem> PostMortems => Set<PostMortem>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        
        var entries = ChangeTracker.Entries<IAuditable>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            var audit = entry.Entity.AuditRecord;
            
            if (audit == null) 
            {
                audit = new AuditRecord();
                entry.Entity.AuditRecord = audit;
            }

            if (entry.State == EntityState.Added)
            {
                audit.CreatedAt = now;
                audit.CreatedBy = _currentUser;
            }

            audit.LastModifiedAt = now;
            audit.LastModifiedBy = _currentUser;
        }

        return await base.SaveChangesAsync(ct);
    }
}