namespace TransPorter.Shared.Interfaces;

public interface IAuditableEntity : IEntity
{
    Guid CreatedUserId { get; set; }
    DateTime CreatedOn { get; set; }
    long? ModifiedUserId { get; set; }
    DateTime? ModifiedOn { get; set; }
}
