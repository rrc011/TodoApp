using Todo.Domain.common;
using Todo.Domain.Entities;

namespace Todo.Application.Features.TodoItems.Commands.UpdateTodoItem
{
    public class TodoItemUpdatedEvent : BaseEvent
    {
        public TodoItem TodoItem { get; set; }

        public TodoItemUpdatedEvent(TodoItem item)
        {
            TodoItem = item;
        }
    }
}
