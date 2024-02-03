using AutoMapper;
using MediatR;
using Todo.Application.Common.Mappings;
using Todo.Application.Features.TodoList.Commands.CreateTodoList;
using Todo.Application.Interfaces;
using Todo.Shared;

namespace Todo.Application.Features.TodoList.Commands
{
    public record CreateTodoListCommand : IRequest<Result<int>>, IMapFrom<Domain.Entities.TodoList>
    {
        public string Title { get; init; }
    }

    internal class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTodoListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
        {
            var todoList = new Domain.Entities.TodoList
            {
                Title = request.Title,
            };

            await _unitOfWork.Repository<Domain.Entities.TodoList>().AddAsync(todoList);
            todoList.AddDomainEvent(new TodoListCreatedEvent(todoList));

            await _unitOfWork.SaveAsync(cancellationToken);

            return await Result<int>.SuccessAsync(todoList.Id, "List Created.");
        }
    }
}
