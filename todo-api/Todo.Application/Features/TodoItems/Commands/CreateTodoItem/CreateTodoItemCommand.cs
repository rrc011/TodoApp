using MediatR;
using Todo.Application.Common.Mappings;
using Todo.Application.Features.TodoItems.Commands.CreateTodoItem;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Shared;

namespace Todo.Application.Features.TodoItems.Commands
{
    public record CreateTodoItemCommand : IRequest<Result<int>>, IMapFrom<Domain.Entities.TodoItem>
    {
        public int ListId { get; init; }

        public string Description { get; init; }
    }

    internal class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateTodoItemCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.Repository<TodoItem>();

            var todoItem = new TodoItem
            {
                ListId = request.ListId,
                Description = request.Description,
            };

            await repo.AddAsync(todoItem);

            await unitOfWork.SaveAsync(cancellationToken);

            todoItem.AddDomainEvent(new TodoItemCreatedEvent(todoItem));

            return await Result<int>.SuccessAsync(todoItem.Id, "Task Created.");
        }
    }
}
