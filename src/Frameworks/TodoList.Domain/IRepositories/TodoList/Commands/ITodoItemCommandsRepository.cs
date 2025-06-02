using TodoList.Domain.Entities;

namespace TodoList.Domain.IRepositories.TodoList.Commands
{
    public interface ITodoItemCommandsRepository
    {
        Task<int> CreateAsync(TodoItem todoItem);
        Task<bool> UpdateAsync(TodoItem todoItem);
        Task<bool> RemoveAsync(int id);
    }
}
