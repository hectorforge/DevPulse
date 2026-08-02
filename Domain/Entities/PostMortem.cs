using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class PostMortem
{
    [Key]
    public string RootCause { get; set; }  = string.Empty;
    public string LessonsLearned { get; set; } = string.Empty;
    public Guid IncidentId { get; set; }
    
    public Incident Incident { get; set; } = null!; 
}