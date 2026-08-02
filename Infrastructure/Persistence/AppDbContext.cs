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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var now = DateTime.UtcNow;
        
        var entries  = ChangeTracker
            .Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            var auditProp = entry.Entity.GetType().GetProperty("AuditRecord");

            if (auditProp != null)
            {
                var auditValue = auditProp.GetValue(entry.Entity) as SimpleAuditRecord;

                if (auditValue != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        auditValue.CreatedAt = now;
                    }

                    auditValue.LastModifiedAt = now;
                }
                
                if (auditValue is AuditRecord fullAudit)
                {
                    if (entry.State == EntityState.Added)
                    {
                        fullAudit.CreatedBy = _currentUser;
                    }
                    fullAudit.LastModifiedBy = _currentUser;
                }
            }
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
}