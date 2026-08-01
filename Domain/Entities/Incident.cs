using Domain.Enums;

namespace Domain.Entities;

public class Incident
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Severity Severity { get; set; }
    public IncidentStatus Status { get; set; } = IncidentStatus.Reported;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ResolvedAt { get; set; }
    
    public PostMortem? PostMortem { get; set; }
}