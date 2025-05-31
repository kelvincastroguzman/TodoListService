using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Persistence;
using TodoList.Infrastructure.Repositories.TodoList.Queries;

namespace TodoList.Infrastructure.Tests.Repositories.TodoList.Queries
{
    public class TodoListQueriesRepositoryTests
    {
        [Fact]
        public async Task GetNextIdAsync_ShouldReturnOne_WhenNoItemsExist()
        {
            var options = CreateInMemoryOptions();
            using var dbContext = new TodoListDbContext(options);
            var repository = new TodoListQueriesRepository(dbContext);

            var nextId = await repository.GetNextIdAsync();

            Assert.Equal(1, nextId);
        }

        [Fact]
        public async Task GetNextIdAsync_ShouldReturnMaxIdPlusOne_WhenItemsExist()
        {
            var options = CreateInMemoryOptions();
            using (var dbContext = new TodoListDbContext(options))
            {
                dbContext.TodoItems.Add(new TodoItem(5)
                {
                    Id = 5,
                    Title = "Title1",
                    Description = "Description1",
                    Category = "Category1"
                });
                dbContext.TodoItems.Add(new TodoItem(10)
                {
                    Id = 10,
                    Title = "Title2",
                    Description = "Description2",
                    Category = "Category2"
                });
                dbContext.SaveChanges();
            }
            using (var dbContext = new TodoListDbContext(options))
            {
                var repository = new TodoListQueriesRepository(dbContext);

                var nextId = await repository.GetNextIdAsync();

                Assert.Equal(11, nextId);
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
