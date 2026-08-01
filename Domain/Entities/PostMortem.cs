namespace Domain.Entities;

public class PostMortem
{
    public string RootCause { get; set; }
    public string LessonsLearned { get; set; }
    public Guid IncidentId { get; set; }
    public Incident Incident { get; set; }
}