namespace TransPorter.Domain.Shared;

public interface IAuditableEntity : IEntity
{
    Guid CreatedUserId { get; set; }
    DateTime CreatedOn { get; set; }
    long? ModifiedUserId { get; set; }
    DateTime? ModifiedOn { get; set; }
}
