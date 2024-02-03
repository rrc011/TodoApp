using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Interfaces;
using Todo.Shared;

namespace Todo.Application.Features.TodoList.Querys
{
    public record GetAllTodoListQuery : IRequest<Result<List<GetAllTodoListDto>>>;

    internal class GetAllTodoLIstQueryHandler : IRequestHandler<GetAllTodoListQuery, Result<List<GetAllTodoListDto>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllTodoLIstQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<List<GetAllTodoListDto>>> Handle(GetAllTodoListQuery request, CancellationToken cancellationToken)
        {
            var list = await unitOfWork.Repository<Domain.Entities.TodoList>().Entities
                   .AsNoTracking()
                   .Where(x => !x.IsDeleted)
                   .Include(x => x.Items.Where(item => !item.IsDeleted))
                   .ProjectTo<GetAllTodoListDto>(mapper.ConfigurationProvider)
                   .OrderBy(x => x.Title)
                   .ToListAsync(cancellationToken);

            return await Result<List<GetAllTodoListDto>>.SuccessAsync(list);
        }
    }
}
