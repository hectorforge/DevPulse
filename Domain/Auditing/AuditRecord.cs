namespace Domain.Common;

public class AuditRecord : SimpleAuditRecord
{
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
}