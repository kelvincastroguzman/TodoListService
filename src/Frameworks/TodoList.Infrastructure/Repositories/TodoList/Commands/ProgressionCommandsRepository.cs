using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Commands;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure.Repositories.TodoList.Commands
{
    public class ProgressionCommandsRepository : IProgressionCommandsRepository
    {
        private readonly DbSet<Progression> _progressions;

        public ProgressionCommandsRepository(TodoListDbContext dbContext)
        {
            _progressions = dbContext.Set<Progression>();
        }

        public void Create(Progression progression)
        {
            _progressions.Add(progression);
        }
    }
}
