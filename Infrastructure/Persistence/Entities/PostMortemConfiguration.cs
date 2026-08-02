using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Entities;

public class PostMortemConfiguration : IEntityTypeConfiguration<PostMortem>
{
    public void Configure(EntityTypeBuilder<PostMortem> builder)
    {
        builder.HasKey(p => p.Id);
        builder.OwnsOne(p => p.AuditRecord);
    }
}