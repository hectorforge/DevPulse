namespace Domain.Common;

public interface IAuditable 
{
    AuditRecord AuditRecord { get; set; }
}