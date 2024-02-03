using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Features.TodoItems.Commands;
using Todo.Application.Features.TodoItems.Querys.GetTodoItems;
using Todo.Shared;

namespace Todo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        [HttpGet]
        public async Task<PaginatedResult<TodoItemDto>> GetTodoItems(ISender sender, [FromQuery] GetTodoItemQuery query)
        {
            return await sender.Send(query);
        }

        [HttpPost]
        public async Task<Result<int>> CreateTodoItem(ISender sender, CreateTodoItemCommand command)
        {
            return await sender.Send(command);
        }

        [HttpPut]
        public async Task<Result<int>> UpdateTodoItem(ISender sender, UpdateTodoItemCommand command)
        {
            return await sender.Send(command);
        }

        [HttpDelete]
        public async Task<Result<int>> DeleteTodoItem(ISender sender, int id)
        {
            return await sender.Send(new DeleteTodoItemCommand(id));
        }
    }
}
