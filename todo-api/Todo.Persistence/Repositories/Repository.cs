using Microsoft.EntityFrameworkCore;
using Todo.Application.Interfaces;
using Todo.Domain.common;
using Todo.Persistence.Contexts;

namespace Todo.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseAuditableEntity
    {
        private readonly TodoDbContext _dbContext;

        public Repository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            T exist = _dbContext.Set<T>().Find(entity.Id);
            entity.IsDeleted = true;
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}
