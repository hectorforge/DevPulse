using Domain.Common;

namespace Domain.Entities;

public class PostMortem : IAuditable
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string RootCause { get; private set; } = string.Empty;
    public string LessonsLearned { get; private set; } = string.Empty;
    public Guid IncidentId { get; private set; }
    public Incident Incident { get; private set; } = null!;
    public AuditRecord AuditRecord { get; set; } = new();
    
    private PostMortem() { }

    public static PostMortem Create(string rootCause, string lessonsLearned, Guid incidentId)
    {
        return new PostMortem
        {
            RootCause = rootCause,
            LessonsLearned = lessonsLearned,
            IncidentId = incidentId
        };
    }

    public void Update(string rootCause, string lessonsLearned)
    {
        RootCause = rootCause;
        LessonsLearned = lessonsLearned;
    }
}