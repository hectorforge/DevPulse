using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Entities;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder.OwnsOne(i => i.AuditRecord);
        
        builder.HasOne(i => i.PostMortem)
            .WithOne(p => p.Incident)
            .HasForeignKey<PostMortem>(p => p.IncidentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}