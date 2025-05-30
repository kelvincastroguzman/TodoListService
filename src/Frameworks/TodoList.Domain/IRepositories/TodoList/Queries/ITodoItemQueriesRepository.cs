using TodoList.Domain.Entities;

namespace TodoList.Domain.IRepositories.TodoList.Queries
{
    public interface ITodoItemQueriesRepository
    {
        Task<IReadOnlyCollection<TodoItem>> GetAllAsync();
    }
}
