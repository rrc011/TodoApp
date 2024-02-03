using MediatR;
using Todo.Application.Common.Mappings;
using Todo.Application.Features.TodoList.Commands.DeleteTodoList;
using Todo.Application.Interfaces;
using Todo.Shared;

namespace Todo.Application.Features.TodoList.Commands
{
    public class DeleteTodoListCommand : IRequest<Result<int>>, IMapFrom<Domain.Entities.TodoList>
    {
        public int Id { get; set; }

        public DeleteTodoListCommand()
        {
            
        }

        public DeleteTodoListCommand(int id)
        {
            Id = id;
        }
    }

    internal class DeletePlayerCommandHandler : IRequestHandler<DeleteTodoListCommand, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeletePlayerCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.Repository<Domain.Entities.TodoList>();
            var todoList = await repo.GetByIdAsync(request.Id);

            if(todoList != null)
            {
                await repo.DeleteAsync(todoList);
                await unitOfWork.SaveAsync(cancellationToken);
                todoList.AddDomainEvent(new TodoDeletedEvent(todoList));
                return await Result<int>.SuccessAsync(todoList.Id, "List Deleted");
            }

            return await Result<int>.FailureAsync("List not found");
        }
    }
}
