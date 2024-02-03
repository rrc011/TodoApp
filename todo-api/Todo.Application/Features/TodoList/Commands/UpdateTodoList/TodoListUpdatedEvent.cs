using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.common;

namespace Todo.Application.Features.TodoList.Commands.UpdateTodoList
{
    public class TodoListUpdatedEvent : BaseEvent
    {
        public Domain.Entities.TodoList TodoList { get; set; }

        public TodoListUpdatedEvent(Domain.Entities.TodoList todoList)
        {
            TodoList = todoList;
        }
    }
}
