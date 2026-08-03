using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class TeamMember : IAuditable<SimpleAuditRecord>
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Role Role { get; private set; }

    public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
    
    public SimpleAuditRecord AuditRecord { get; set; } = new();

    private TeamMember()
    {
        // EF
    }
    
    public static TeamMember Create(
        string Name,
        string Email,
        Role Role)
    {
        if (string.IsNullOrWhiteSpace(Name)) 
            throw new ArgumentException("Name can not be null or empty");    
        if (string.IsNullOrWhiteSpace(Email))
            throw new ArgumentException("Email can not be null or empty");    

        var teamMember = new TeamMember
        {
            Id = Guid.NewGuid(),
            Name = Name.Trim(),
            Email = Email.Trim(),
            Role = Role
        };
        
        return teamMember;
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name can not be null or empty");
        Name = name.Trim();
    }

    public void ChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email can not be null or empty");
        Email = email.Trim();
    }

    public void ChangeRole(Role role)
    {
        if (role == null)
        {
            throw new ArgumentException("Role can not be null or empty");
        }
        Role = role;
    }
}