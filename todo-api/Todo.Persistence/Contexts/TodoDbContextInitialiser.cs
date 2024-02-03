using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Todo.Domain.Entities;

namespace Todo.Persistence.Contexts
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<TodoDbContextInitialiser>();

            await initialiser.InitialiseAsync();

            await initialiser.SeedAsync();
        }
    }

    public class TodoDbContextInitialiser
    {
        private readonly ILogger<TodoDbContextInitialiser> logger;
        private readonly TodoDbContext context;

        public TodoDbContextInitialiser(ILogger<TodoDbContextInitialiser> logger, TodoDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default data
            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Todo List",
                    Items =
                {
                    new TodoItem { Description = "Make a todo list 📃" },
                    new TodoItem { Description = "Check off the first item ✅" },
                    new TodoItem { Description = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Description = "Reward yourself with a nice, long nap 🏆" },
                }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
