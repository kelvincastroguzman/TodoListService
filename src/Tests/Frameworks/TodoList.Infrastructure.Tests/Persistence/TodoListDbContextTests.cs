using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure.Tests.Persistence
{
    public class TodoListDbContextTests
    {
        [Fact]
        public void CanCreateDbContext()
        {
            var options = CreateInMemoryOptions();
            using var context = new TodoListDbContext(options);
            Assert.NotNull(context);
        }

        [Fact]
        public void DbSets_AreAccessible()
        {
            var options = CreateInMemoryOptions();
            using var context = new TodoListDbContext(options);
            Assert.NotNull(context.Categories);
            Assert.NotNull(context.Progressions);
            Assert.NotNull(context.TodoItems);
        }

        [Fact]
        public void CanAddAndRetrieveEntities()
        {
            var options = CreateInMemoryOptions();
            using (var context = new TodoListDbContext(options))
            {
                var category = new Category { Name = "Work" };
                var todoItem = new TodoItem(1) { Id = 1, Title = "Title", Description = "Description", Category = "Work" };
                context.Categories.Add(category);
                context.TodoItems.Add(todoItem);
                context.SaveChanges();
            }
            using (var context = new TodoListDbContext(options))
            {
                Assert.Single(context.Categories);
                Assert.Single(context.TodoItems);
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
