using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Incident>  Incidents { get; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Incident>()
            .HasOne(i => i.PostMortem)
            .WithOne(p => p.Incident)
            .HasForeignKey<PostMortem>(p => p.IncidentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}