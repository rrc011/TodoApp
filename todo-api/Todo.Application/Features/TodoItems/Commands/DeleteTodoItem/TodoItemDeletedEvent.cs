using Todo.Domain.common;
using Todo.Domain.Entities;

namespace Todo.Application.Features.TodoItems.Commands.DeleteTodoItem
{
    public class TodoItemDeletedEvent : BaseEvent
    {
        public TodoItem TodoItem { get; set; }

        public TodoItemDeletedEvent(TodoItem item)
        {
            TodoItem = item;
        }
    }
}
