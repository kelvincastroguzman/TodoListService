using TodoList.Application.Dtos;

namespace TodoList.Application.TodoList.Queries.Contracts
{
    public interface ITodoListQueriesService
    {
        IReadOnlyCollection<TodoItemDto> PrintItems();
    }
}
