using TodoList.Application.Dtos;
using TodoList.Application.TodoList.Queries.Contracts;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Queries;

namespace TodoList.Application.TodoList.Queries
{
    internal class TodoListQueriesService : ITodoListQueriesService
    {
        private readonly ITodoItemQueriesRepository _todoItemQueriesRepository;
        private readonly IProgressionQueriesRepository _progressionQueriesRepository;

        public TodoListQueriesService(ITodoItemQueriesRepository todoItemQueriesRepository, IProgressionQueriesRepository progressionQueriesRepository)
        {
            _todoItemQueriesRepository = todoItemQueriesRepository;
            _progressionQueriesRepository = progressionQueriesRepository;
        }

        public async Task<IReadOnlyCollection<string>> PrintItems()
        {
            IReadOnlyCollection<TodoItem> todoItems = await _todoItemQueriesRepository.GetAllAsync();

            var response = new List<string>();

            foreach (TodoItem todoItem in todoItems.OrderBy(i => i.Id))
            {
                IReadOnlyCollection<Progression> progressions = await _progressionQueriesRepository.GetByTodoItemIdAsync(todoItem.Id);
                var dto = new TodoItemDto
                {
                    Id = todoItem.Id,
                    Title = todoItem.Title,
                    Description = todoItem.Description,
                    Category = todoItem.Category,
                    IsCompleted = IsCompleted(),
                    Progressions = todoItem.Progressions.Select(p => new ProgressionDto
                    {
                        Date = p.Date,
                        Percent = p.Percent
                    }).ToList()
                };
                response.Add($"{dto.Id}) {dto.Title} - {dto.Description} ({dto.Category}) Completed:{dto.IsCompleted}.");
            }

            return response.AsReadOnly();
        }

        private bool IsCompleted()
        {
            return true;
        }
    }
}
