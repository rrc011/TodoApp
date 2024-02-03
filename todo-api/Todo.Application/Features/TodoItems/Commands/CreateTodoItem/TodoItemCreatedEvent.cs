using Todo.Domain.common;

namespace Todo.Application.Features.TodoItems.Commands.CreateTodoItem
{
    public class TodoItemCreatedEvent : BaseEvent
    {
        public Domain.Entities.TodoItem TodoItem { get; }

        public TodoItemCreatedEvent(Domain.Entities.TodoItem todoItem)
        {
            TodoItem = todoItem;
        }
    }
}
