using Todo.Application.Common.Mappings;
using Todo.Application.Features.TodoItems.Querys.GetTodoItems;

namespace Todo.Application.Features.TodoList.Querys
{
    public class GetAllTodoListDto : IMapFrom<Domain.Entities.TodoList>
    {
        public GetAllTodoListDto()
        {
            Items = Array.Empty<TodoItemDto>();
        }

        public int Id { get; init; }

        public string Title { get; init; }

        public IReadOnlyCollection<TodoItemDto> Items { get; init; }
    }
}
