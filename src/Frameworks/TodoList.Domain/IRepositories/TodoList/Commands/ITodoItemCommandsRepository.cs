using TodoList.Domain.Entities;

namespace TodoList.Domain.IRepositories.TodoList.Commands
{
    public interface ITodoItemCommandsRepository
    {
        void Create(TodoItem todoItem);
        void Update(TodoItem todoItem);
        void Remove(int id);
    }
}
