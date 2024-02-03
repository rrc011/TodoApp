using MediatR;
using Todo.Application.Common.Mappings;
using Todo.Application.Features.TodoItems.Commands.DeleteTodoItem;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Shared;

namespace Todo.Application.Features.TodoItems.Commands
{
    public class DeleteTodoItemCommand : IRequest<Result<int>>, IMapFrom<TodoItem>
    {
        public int Id { get; set; }

        public DeleteTodoItemCommand()
        {
            
        }

        public DeleteTodoItemCommand(int todoItemId)
        {
            Id = todoItemId;
        }

    }

    internal class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteTodoItemCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.Repository<TodoItem>();

            var item = await repo.GetByIdAsync(request.Id);

            if (item != null)
            {
                await repo.DeleteAsync(item);
                await unitOfWork.SaveAsync(cancellationToken);
                item.AddDomainEvent(new TodoItemDeletedEvent(item));

                return await Result<int>.SuccessAsync(item.Id, "Task Deleted");
            }

            return await Result<int>.FailureAsync("Task not found");
        }
    }
}
