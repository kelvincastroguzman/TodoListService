using TodoList.Domain.Entities;

namespace TodoList.Domain.IRepositories.TodoList.Queries
{
    public interface IProgressionQueriesRepository
    {
        Task<IReadOnlyCollection<Progression>> GetByTodoItemIdAsync(int todoItemId);
    }
}
