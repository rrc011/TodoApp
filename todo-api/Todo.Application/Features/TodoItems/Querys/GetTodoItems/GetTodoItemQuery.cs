using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Todo.Application.Extensions;
using Todo.Application.Interfaces;
using Todo.Domain.Entities;
using Todo.Shared;

namespace Todo.Application.Features.TodoItems.Querys.GetTodoItems
{
    public class GetTodoItemQuery : IRequest<PaginatedResult<TodoItemDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetTodoItemQuery()
        {
            
        }

        public GetTodoItemQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    internal class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, PaginatedResult<TodoItemDto>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetTodoItemQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<PaginatedResult<TodoItemDto>> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.Repository<TodoItem>();

            return await repo.Entities.OrderByDescending(x => x.CreatedDate)
                .Where(x => !x.IsDeleted)
                .ProjectTo<TodoItemDto>(mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
