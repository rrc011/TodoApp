using Todo.Domain.common;

namespace Todo.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : BaseAuditableEntity;
        Task<int> SaveAsync(CancellationToken cancellationToken);
        Task Rollback();
    }
}
