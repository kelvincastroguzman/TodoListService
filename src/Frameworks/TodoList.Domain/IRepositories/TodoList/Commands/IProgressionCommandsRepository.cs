using TodoList.Domain.Entities;

namespace TodoList.Domain.IRepositories.TodoList.Commands
{
    public interface IProgressionCommandsRepository
    {
        void Create(Progression progression);
    }
}
