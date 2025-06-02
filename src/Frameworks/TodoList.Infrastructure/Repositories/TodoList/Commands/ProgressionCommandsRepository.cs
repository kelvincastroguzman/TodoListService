using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Commands;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure.Repositories.TodoList.Commands
{
    public class ProgressionCommandsRepository : IProgressionCommandsRepository
    {
        private readonly TodoListDbContext _dbContext;
        private readonly DbSet<Progression> _progressions;

        public ProgressionCommandsRepository(TodoListDbContext dbContext)
        {
            _dbContext = dbContext;
            _progressions = dbContext.Set<Progression>();
        }

        public async Task<int> CreateAsync(Progression progression)
        {
            await _progressions.AddAsync(progression);
            if (await _dbContext.SaveChangesAsync() <= 0)
            {
                throw new InvalidOperationException("Failed to create progression. Please try again later.");
            }

            return progression.Id;
        }
    }
}
