namespace Todo.Domain.common.Interfaces
{
    public interface IAuditableEntity : IEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
