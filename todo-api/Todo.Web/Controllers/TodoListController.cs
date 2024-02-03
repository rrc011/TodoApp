using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Features.TodoList.Commands;
using Todo.Application.Features.TodoList.Querys;
using Todo.Shared;

namespace Todo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        [HttpGet]
        public async Task<Result<List<GetAllTodoListDto>>> GetTodoLists(ISender sender)
        {
            return await sender.Send(new GetAllTodoListQuery());
        }

        [HttpPost]
        public async Task<Result<int>> CreateTodoList(ISender sender, CreateTodoListCommand command)
        {
            return await sender.Send(command);
        }

        [HttpPut]
        public async Task<Result<int>> UpdateTodoList(ISender sender, UpdateTodoListCommand command)
        {
            return await sender.Send(command);
        }

        [HttpDelete]
        public async Task<Result<int>> DeleteTodoList(ISender sender, int id)
        {
            return await sender.Send(new DeleteTodoListCommand(id));
        }
    }
}
