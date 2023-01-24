namespace TransPorter.Domain.Shared;

internal class AuditableEntity : IAuditableEntity
{
    public virtual Guid Id { get; set; }
    public virtual Guid CreatedUserId { get; set; }
    public DateTime CreatedOn { get; set; }
    public long? ModifiedUserId { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public bool IsActive { get; set; }
}
