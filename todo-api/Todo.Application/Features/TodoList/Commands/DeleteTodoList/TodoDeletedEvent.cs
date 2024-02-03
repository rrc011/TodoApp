using Todo.Domain.common;

namespace Todo.Application.Features.TodoList.Commands.DeleteTodoList
{
    public class TodoDeletedEvent : BaseEvent
    {
        public Domain.Entities.TodoList TodoList { get; set; }
        public TodoDeletedEvent(Domain.Entities.TodoList todoList)
        {
            TodoList = todoList;
        }
    }
}
