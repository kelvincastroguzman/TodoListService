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

        public async Task<IReadOnlyCollection<TodoItemDto>> PrintItems()
        {
            IReadOnlyCollection<TodoItem> todoItems = await _todoItemQueriesRepository.GetAllAsync();

            var response = new List<TodoItemDto>();

            foreach (TodoItem todoItem in todoItems)
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
                        Id = p.Id,
                        Date = p.Date,
                        Percent = p.Percent
                    }).ToList()
                };
                response.Add(dto);
            }

            return response.AsReadOnly();
        }

        private bool IsCompleted()
        {
            return true;
        }
    }
}
