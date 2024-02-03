using Todo.Domain.common;

namespace Todo.Domain.Entities
{
    public class TodoItem : BaseAuditableEntity
    {
        public int ListId { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public virtual TodoList List { get; set; }
    }
}
