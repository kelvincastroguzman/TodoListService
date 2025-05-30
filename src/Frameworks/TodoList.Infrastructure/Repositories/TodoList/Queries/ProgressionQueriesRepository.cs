using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Queries;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure.Repositories.TodoList.Queries
{
    public class ProgressionQueriesRepository : IProgressionQueriesRepository
    {
        private readonly DbSet<Progression> _progressions;

        public ProgressionQueriesRepository(TodoListDbContext dbContext)
        {
            _progressions = dbContext.Set<Progression>();
        }

        public async Task<IReadOnlyCollection<Progression>> GetByTodoItemIdAsync(int todoItemId)
        {
            return await _progressions
                .AsNoTracking()
                .Where(p => p.TodoItemId == todoItemId)
                .ToListAsync();
        }
    }
}
