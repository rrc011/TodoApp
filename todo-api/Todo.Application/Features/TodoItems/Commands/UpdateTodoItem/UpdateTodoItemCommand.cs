using MediatR;
using Todo.Application.Common.Mappings;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Shared;

namespace Todo.Application.Features.TodoItems.Commands
{
    public class UpdateTodoItemCommand : IRequest<Result<int>>, IMapFrom<TodoItem>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }

    internal class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, Result<int>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateTodoItemCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.Repository<TodoItem>();

            var item = await repo.GetByIdAsync(request.Id);

            if (item != null)
            {
                item.Description = request.Description;
                item.Done = request.Done;

                await repo.UpdateAsync(item);

                await unitOfWork.SaveAsync(cancellationToken);

                return await  Result<int>.SuccessAsync("Task Updated");
            }

            return await Result<int>.FailureAsync("Task not Found");
        }
    }
}
