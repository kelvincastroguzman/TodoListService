using TodoList.Application.Enums;
using TodoList.Application.TodoList.Validators.Contracts;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Queries;

namespace TodoList.Application.TodoList.Validators
{
    internal class CreateTodoItemValidator : ITodoListValidator
    {
        private readonly IProgressionQueriesRepository _progressionQueriesRepository;

        public CreateTodoItemValidator(IProgressionQueriesRepository progressionQueriesRepository)
        {
            _progressionQueriesRepository = progressionQueriesRepository;
        }

        public bool IsApplicable(TodoListActions action)
        {
            return action == TodoListActions.Create;
        }

        public void Validate(TodoItem todoItem)
        {
            if (todoItem.Id <= 0)
            {
                throw new ArgumentException("Id must be a positive integer.", nameof(todoItem.Id));
            }

            if (string.IsNullOrWhiteSpace(todoItem.Title))
            {
                throw new ArgumentException("Title cannot be empty.", nameof(todoItem.Title));
            }

            if (string.IsNullOrWhiteSpace(todoItem.Description))
            {
                throw new ArgumentException("Description cannot be empty.", nameof(todoItem.Description));
            }

            if (string.IsNullOrWhiteSpace(todoItem.Category))
            {
                throw new ArgumentException("Category cannot be empty.", nameof(todoItem.Category));
            }

            var existingProgressions = _progressionQueriesRepository.GetByTodoItemIdAsync(todoItem.Id).Result;

            todoItem.Progressions?.ToList().ForEach(progression =>
            {
                if (existingProgressions.Any(e => e.Date >= progression.Date))
                {
                    throw new ArgumentException("Progression date must be greater than existing dates.", nameof(progression.Date));
                }

                if (progression.Percent < Constants.Constants.Validations.TodoItem.Progression.MIN_PERCENT 
                    || progression.Percent >= Constants.Constants.Validations.TodoItem.Progression.MAX_PERCENT)
                {
                    throw new ArgumentException("Progression percent must be between 1 and 99.", nameof(progression.Percent));
                }
            });

            int totalPercent = existingProgressions?.Sum(p => p.Percent) ?? 0;

            if (totalPercent + todoItem.Progressions?.Sum(p => p.Percent) > Constants.Constants.Validations.TodoItem.Progression.MAX_PERCENT)
            {
                throw new ArgumentException("Total progress percentage cannot exceed 100.", nameof(todoItem.Progressions));
            }
        }
    }
}
