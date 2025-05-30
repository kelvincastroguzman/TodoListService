namespace TodoList.Domain.IRepositories.TodoList.Queries
{
    public interface ITodoListQueriesRepository
    {
        Task<int> GetNextIdAsync();
        Task<IReadOnlyCollection<string>> GetAllCategoriesAsync();
    }
}
