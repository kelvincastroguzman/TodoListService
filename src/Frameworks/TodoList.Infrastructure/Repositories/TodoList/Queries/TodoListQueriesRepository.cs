using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Queries;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure.Repositories.TodoList.Queries
{
    public class TodoListQueriesRepository : ITodoListQueriesRepository
    {
        private readonly DbSet<Category> _categories;
        private readonly DbSet<TodoItem> _todoItems;

        public TodoListQueriesRepository(TodoListDbContext dbContext)
        {
            _categories = dbContext.Set<Category>();
            _todoItems = dbContext.Set<TodoItem>();
        }

        public async Task<IReadOnlyCollection<string>> GetAllCategoriesAsync()
        {
            return await _categories
                    .AsNoTracking()
                    .Select(c => c.Name)
                    .ToListAsync();
        }

        public async Task<int> GetNextIdAsync()
        {
            int maxId = await _todoItems.MaxAsync(t => (int?)t.Id) ?? 0;
            return maxId + 1;
        }

    }
}
