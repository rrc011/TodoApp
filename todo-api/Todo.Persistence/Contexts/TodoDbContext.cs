using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using Todo.Domain.common;
using Todo.Domain.common.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Persistence.Contexts
{
    public class TodoDbContext : DbContext
    {
        private readonly IDomainEventDispatcher dispatcher;

        public TodoDbContext(DbContextOptions<TodoDbContext> options,
          IDomainEventDispatcher dispatcher) : base(options)
        {
            this.dispatcher = dispatcher;
        }

        public DbSet<TodoList> TodoLists => Set<TodoList>();

        public DbSet<TodoItem> TodoItems => Set<TodoItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<TodoItem>().HasQueryFilter(item => !item.IsDeleted);
            modelBuilder.Entity<TodoList>().HasQueryFilter(item => !item.IsDeleted);

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            await dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
