using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class TeamMember : IAuditable<SimpleAuditRecord>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }

    public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
    
    public SimpleAuditRecord AuditRecord { get; set; } = new();
}