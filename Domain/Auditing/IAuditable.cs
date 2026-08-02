namespace Domain.Common;

public interface IAuditable<TAudit> where TAudit : SimpleAuditRecord
{
    TAudit AuditRecord { get; set; }
}