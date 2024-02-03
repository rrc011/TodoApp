using AutoMapper;
using MediatR;
using Todo.Application.Features.TodoList.Commands.UpdateTodoList;
using Todo.Application.Interfaces;
using Todo.Shared;

namespace Todo.Application.Features.TodoList.Commands
{
    public record UpdateTodoListCommand : IRequest<Result<int>>
    {
            public int Id { get; set; }
            public string Title { get; set; }
    }
    internal class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand, Result<int>>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly IMapper mapper;

            public UpdateTodoListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitOfWork = unitOfWork;
                this.mapper = mapper;
            }

            public async Task<Result<int>> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
            {
                var repo = unitOfWork.Repository<Domain.Entities.TodoList>();

                var todoList = await repo.GetByIdAsync(request.Id);

                if(todoList != null)
                {
                    todoList.Title = request.Title;
                    await repo.UpdateAsync(todoList);
                    todoList.AddDomainEvent(new TodoListUpdatedEvent(todoList));

                    await unitOfWork.SaveAsync(cancellationToken);

                    return await Result<int>.SuccessAsync(todoList.Id, "List Updated");
                }

                return await Result<int>.FailureAsync("List not found");
            }
        }
}
