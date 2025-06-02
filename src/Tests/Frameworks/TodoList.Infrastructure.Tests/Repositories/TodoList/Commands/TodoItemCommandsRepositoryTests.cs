using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Infrastructure.Persistence;
using TodoList.Infrastructure.Repositories.TodoList.Commands;

namespace TodoList.Infrastructure.Tests.Repositories.TodoList.Commands
{
    public class TodoItemCommandsRepositoryTests
    {
        [Fact]
        public async void Create_AddsTodoItemToDbSet()
        {
            using var dbContext = CreateDbContext();
            var repository = new TodoItemCommandsRepository(dbContext);
            var todoItem = new TodoItem(1) { Id = 1, Title = "Title1", Description = "Description1", Category = "Category1" };

            await repository.CreateAsync(todoItem);
            await dbContext.SaveChangesAsync();

            var saved = dbContext.TodoItems.Find(1);
            Assert.NotNull(saved);
            Assert.Equal("Title1", saved.Title);
        }

        [Fact]
        public void Create_ThrowsArgumentNullException_WhenTodoItemIsNull()
        {
            using var dbContext = CreateDbContext();
            var repository = new TodoItemCommandsRepository(dbContext);

            Assert.ThrowsAsync<NullReferenceException>(() => repository.CreateAsync(null!));
        }

        [Fact]
        public async void Update_ChangesDescriptionOfExistingTodoItem()
        {
            using var dbContext = CreateDbContext();
            dbContext.TodoItems.Add(new TodoItem(2) { Id = 2, Title = "Title2", Description = "Old", Category = "Category2" });
            await dbContext.SaveChangesAsync();

            var repository = new TodoItemCommandsRepository(dbContext);
            var updatedItem = new TodoItem(2) { Id = 2, Title = "Title2", Description = "New", Category = "Category2" };

            await repository.UpdateAsync(updatedItem);
            await dbContext.SaveChangesAsync();

            TodoItem? saved = dbContext.TodoItems.Find(2);
            Assert.NotNull(saved);
            Assert.Equal("New", saved.Description);
        }

        [Fact]
        public void Update_DoesNothing_WhenTodoItemDoesNotExist()
        {
            using var dbContext = CreateDbContext();
            var repository = new TodoItemCommandsRepository(dbContext);
            var nonExistentItem = new TodoItem(99) { Id = 99, Title = "T", Description = "D", Category = "C" };

            Assert.ThrowsAsync<InvalidOperationException>(() => repository.UpdateAsync(nonExistentItem));
            TodoItem? result = dbContext.TodoItems.Find(99);
            Assert.Null(result);
        }

        [Fact]
        public async void Remove_DeletesTodoItemById()
        {
            using var dbContext = CreateDbContext();
            dbContext.TodoItems.Add(new TodoItem(3) { Id = 3, Title = "Title3", Description = "Description3", Category = "Category3" });
            await dbContext.SaveChangesAsync();

            var repository = new TodoItemCommandsRepository(dbContext);
            await repository.RemoveAsync(3);
            await dbContext.SaveChangesAsync();

            var deleted = dbContext.TodoItems.Find(3);
            Assert.Null(deleted);
        }

        [Fact]
        public void Remove_DoesNothing_WhenTodoItemDoesNotExist()
        {
            using var dbContext = CreateDbContext();
            var repository = new TodoItemCommandsRepository(dbContext);

            Assert.ThrowsAsync<InvalidOperationException>(() => repository.RemoveAsync(99));
            Assert.Empty(dbContext.TodoItems);
        }

        private TodoListDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<TodoListDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new TodoListDbContext(options);
        }
    }
}
