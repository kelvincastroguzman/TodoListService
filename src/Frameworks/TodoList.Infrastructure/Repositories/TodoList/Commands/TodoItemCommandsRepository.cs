using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Commands;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure.Repositories.TodoList.Commands
{
    public class TodoItemCommandsRepository : ITodoItemCommandsRepository
    {
        private readonly TodoListDbContext _dbContext;
        private readonly DbSet<TodoItem> _todoItems;
        private readonly DbSet<Progression> _progressions;

        public TodoItemCommandsRepository(TodoListDbContext dbContext)
        {
            _dbContext = dbContext;
            _todoItems = dbContext.Set<TodoItem>();
            _progressions = dbContext.Set<Progression>();
        }

        public async Task<int> CreateAsync(TodoItem todoItem)
        {
            await _todoItems.AddAsync(todoItem);
            if (await _dbContext.SaveChangesAsync() <= 0)
            {
                throw new InvalidOperationException("Failed to create TodoItem. Please try again later.");
            }

            return todoItem.Id;
        }

        public async Task<bool> UpdateAsync(TodoItem todoItem)
        {
            TodoItem? todoItemResponse = await _todoItems.FindAsync(todoItem.Id);

            if (todoItemResponse is null)
                throw new InvalidOperationException("TodoItem not found for update.");

            try
            {
                todoItemResponse.Description = todoItem.Description;
                _todoItems.Update(todoItemResponse);
                if (await _dbContext.SaveChangesAsync() <= 0)
                {
                    throw new InvalidOperationException("Failed to update TodoItem. Please try again later.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("The item was already modified by another user.");
            }

            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var todoItem = await _todoItems
                .Include(t => t.Progressions)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (todoItem is null)
                throw new InvalidOperationException("TodoItem not found for removal.");

            try
            {
                if (todoItem.Progressions != null && todoItem.Progressions.Any())
                {
                    _progressions.RemoveRange(todoItem.Progressions);
                }

                _todoItems.Remove(todoItem);

                if (await _dbContext.SaveChangesAsync() <= 0)
                {
                    throw new InvalidOperationException("Failed to remove TodoItem. Please try again later.");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("The item was already deleted or modified by another user.");
            }

            return true;
        }
    }
}
