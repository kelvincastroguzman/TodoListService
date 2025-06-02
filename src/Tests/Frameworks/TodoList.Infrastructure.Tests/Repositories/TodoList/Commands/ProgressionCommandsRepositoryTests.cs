using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Persistence;
using TodoList.Infrastructure.Repositories.TodoList.Commands;

namespace TodoList.Infrastructure.Tests.Repositories.TodoList.Commands
{
    public class ProgressionCommandsRepositoryTests
    {
        [Fact]
        public async void Create_AddsProgressionToDbSet()
        {
            DateTime dateTime = DateTime.Now;
            var options = CreateInMemoryOptions();
            using (TodoListDbContext dbContext = new TodoListDbContext(options))
            {
                var repository = new ProgressionCommandsRepository(dbContext);
                var progression = new Progression(1) { Id = 1, TodoItemId = 1, Date = dateTime, Percent = 20 };

                await repository.CreateAsync(progression);
                Assert.Contains(progression, dbContext.Set<Progression>());
            }
        }

        [Fact]
        public async void Create_PersistsProgressionAfterSaveChanges()
        {
            DateTime dateTime = DateTime.Now;
            var options = CreateInMemoryOptions();

            TodoListDbContext dbContext;

            using (dbContext = new TodoListDbContext(options))
            {
                dbContext.TodoItems.Add(new TodoItem(2)
                {
                    Id = 2,
                    Title = "Title2",
                    Description = "Description2",
                    Category = "Category2"
                });
                dbContext.SaveChanges();

                var repository = new ProgressionCommandsRepository(dbContext);

                var progression = new Progression(2) { Id = 2, TodoItemId = 2, Date = dateTime, Percent = 20 };
                await repository.CreateAsync(progression);

                dbContext.SaveChanges();

                var saved = dbContext.Set<Progression>().Find(2);
                Assert.NotNull(saved);
                Assert.Equal(2, saved.Id);
                Assert.Equal(2, saved.TodoItemId);
                Assert.Equal(dateTime, saved.Date);
                Assert.Equal(20, saved.Percent);
            }
        }

        private DbContextOptions<TodoListDbContext> CreateInMemoryOptions()
        {
            return new DbContextOptionsBuilder<TodoListDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }
    }
}
