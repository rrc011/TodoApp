using Todo.Domain.common.Interfaces;

namespace Todo.Domain.common
{
    public class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
