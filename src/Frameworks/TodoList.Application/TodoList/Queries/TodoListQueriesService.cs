using TodoList.Application.Dtos;
using TodoList.Application.TodoList.Queries.Contracts;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Queries;

namespace TodoList.Application.TodoList.Queries
{
    internal class TodoListQueriesService : ITodoListQueriesService
    {
        private readonly ITodoItemQueriesRepository _todoItemQueriesRepository;

        public TodoListQueriesService(ITodoItemQueriesRepository todoItemQueriesRepository)
        {
            _todoItemQueriesRepository = todoItemQueriesRepository;
        }

        IReadOnlyCollection<TodoItemDto> ITodoListQueriesService.PrintItems()
        {
            IReadOnlyCollection<TodoItem> todoItems = _todoItemQueriesRepository.GetAllAsync().Result;

            var response = new List<TodoItemDto>();

            foreach (TodoItem todoItem in todoItems.OrderBy(i => i.Id))
            {
                response.Add(new TodoItemDto
                {
                    Id = todoItem.Id,
                    Title = todoItem.Title,
                    Description = todoItem.Description,
                    Category = todoItem.Category,
                    IsCompleted = IsCompleted(todoItem),
                    Progressions = [.. todoItem.Progressions.Select(p => new ProgressionDto
                    {
                        Date = p.Date,
                        Percent = p.Percent
                    })]
                });
            }

            return response.AsReadOnly();
        }

        private bool IsCompleted(TodoItem todoItem)
        {
            int totalPercent = todoItem.Progressions?.Sum(p => p.Percent) ?? 0;
            return totalPercent == Constants.Constants.Validations.TodoItem.Progression.MAX_PERCENT;
        }
    }
}
