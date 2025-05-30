using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Queries;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure.Repositories.TodoList.Queries
{
    public class TodoItemQueriesRepository : ITodoItemQueriesRepository
    {
        private readonly DbSet<TodoItem> _todoItems;

        public TodoItemQueriesRepository(TodoListDbContext dbContext)
        {
            _todoItems = dbContext.Set<TodoItem>();
        }

        public async Task<IReadOnlyCollection<TodoItem>> GetAllAsync()
        {
            return await _todoItems
                    .AsNoTracking()
                    .ToListAsync();
        }
    }
}
