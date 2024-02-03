using Todo.Domain.common;

namespace Todo.Application.Features.TodoList.Commands.CreateTodoList
{
    public class TodoListCreatedEvent : BaseEvent
    {
        public Domain.Entities.TodoList TodoList { get; }

        public TodoListCreatedEvent(Domain.Entities.TodoList todoList)
        {
            TodoList = todoList;
        }
    }
}
