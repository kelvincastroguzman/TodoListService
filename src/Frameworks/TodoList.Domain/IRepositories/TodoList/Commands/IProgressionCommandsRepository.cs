namespace TodoList.Domain.IRepositories.TodoList.Commands
{
    public interface IProgressionCommandsRepository
    {
        void Create(int id, DateTime dateTime, decimal percent);
    }
}
