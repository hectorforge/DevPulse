using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Incident : IAuditable
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Severity Severity { get; private set; }
    public IncidentStatus Status { get; private set; }
    public DateTime ReportedAt { get; private set; }
    public DateTime ExpectedResolutionAt { get; private set; }
    public DateTime? ResolvedAt { get; private set; }
    public PostMortem? PostMortem { get; private set; }
        
    public Guid? AssignedToId { get; private set; }
    public TeamMember? AssignedTo { get; private set; }
    
    public AuditRecord AuditRecord { get; set; } = new();

    private Incident()
    {
    }

    public static Incident Create(
        string title, 
        string description, 
        Severity severity,
        DateTime reportedAt)
    {
        var incident = new Incident
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Severity = severity,
            Status = IncidentStatus.Reported,
            ReportedAt = reportedAt
        };

        incident.UpdateExpectedResolution();

        return incident;
    }

    public void ChangeTitle(string title)
    {
        Title = title;
    }

    public void ChangeDescription(string description)
    {
        Description = description;
    }
    
    public void ChangeSeverity(Severity severity)
    {
        Severity = severity;
        UpdateExpectedResolution();
    }

    public void ChangeStatus(IncidentStatus status)
    {
        Status = status;
    }
    
    public void Resolve(DateTime resolvedAt)
    {
        Status = IncidentStatus.Resolved;
        ResolvedAt = resolvedAt;
    }
    
    public void StartInvestigation()
    {
        Status = IncidentStatus.InProgress;
    }
    
    public void AttachPostMortem(PostMortem postMortem)
    {
        PostMortem = postMortem;
    }
    
    public void AssignTo(TeamMember member)
    {
        AssignedTo = member;
        AssignedToId = member.Id;
    }

    public void Unassign()
    {
        AssignedTo = null;
        AssignedToId = null;
    }
    
    private void UpdateExpectedResolution()
    {
        ExpectedResolutionAt = Severity switch
        {
            Severity.Critical => ReportedAt.AddHours(1),
            Severity.High => ReportedAt.AddHours(4),
            Severity.Medium => ReportedAt.AddDays(1),
            Severity.Low => ReportedAt.AddDays(3),
            _ => ReportedAt.AddDays(7)
        };
    }
}