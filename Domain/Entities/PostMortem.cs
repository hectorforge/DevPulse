using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities;

public class PostMortem :  IAuditable<AuditRecord>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RootCause { get; set; } = string.Empty;
    public string LessonsLearned { get; set; } = string.Empty;
    public Guid IncidentId { get; set; }
    public Incident Incident { get; set; } = null!;
    
    public AuditRecord AuditRecord { get; set; } = new();
}