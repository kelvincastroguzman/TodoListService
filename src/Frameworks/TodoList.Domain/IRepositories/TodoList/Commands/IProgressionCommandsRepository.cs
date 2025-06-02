using TodoList.Domain.Entities;

namespace TodoList.Domain.IRepositories.TodoList.Commands
{
    public interface IProgressionCommandsRepository
    {
        Task<int> CreateAsync(Progression progression);
    }
}
