using AutoMapper;
using Todo.Domain.Entities;

namespace Todo.Application.Features.TodoItems.Querys.GetTodoItems
{
    public class TodoItemDto
    {
        public int Id { get; init; }

        public int ListId { get; init; }

        public string Description { get; init; }

        public bool Done { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<TodoItem, TodoItemDto>();
            }
        }
    }
}
