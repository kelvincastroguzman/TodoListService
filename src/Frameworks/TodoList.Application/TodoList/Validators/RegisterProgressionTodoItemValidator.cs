using TodoList.Application.Enums;
using TodoList.Application.TodoList.Validators.Contracts;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Queries;

namespace TodoList.Application.TodoList.Validators
{
    internal class RegisterProgressionTodoItemValidator : ITodoListValidator
    {
        private readonly IProgressionQueriesRepository _progressionQueriesRepository;

        public RegisterProgressionTodoItemValidator(IProgressionQueriesRepository progressionQueriesRepository)
        {
            _progressionQueriesRepository = progressionQueriesRepository;
        }

        bool ITodoListValidator.IsApplicable(TodoListActions action)
        {
            return action == TodoListActions.RegisterProgression;
        }

        void ITodoListValidator.Validate(TodoItem todoItem)
        {
            var existingProgressions = _progressionQueriesRepository.GetByTodoItemIdAsync(todoItem.Id).Result;

            todoItem.Progressions?.ToList().ForEach(progression =>
            {
                if (existingProgressions.Any(e => e.Date >= progression.Date))
                {
                    throw new ArgumentException($"{nameof(progression.Date)} must be greater than existing dates.");
                }

                if (progression.Percent < Constants.Constants.Validations.TodoItem.Progression.MIN_PERCENT
                    || progression.Percent >= Constants.Constants.Validations.TodoItem.Progression.MAX_PERCENT)
                {
                    throw new ArgumentException($"{nameof(progression.Percent)} must be between 1 and 99. ");
                }
            });

            int currentPercent = existingProgressions?.Sum(p => p.Percent) ?? 0;

            if (currentPercent > Constants.Constants.Validations.TodoItem.Progression.PERCENT_NOT_ALLOWED_FOR_MODIFICATIONS)
            {
                throw new ArgumentException($"Todo-item cannot be updated. The current percentage ({currentPercent}) is higher than 50.");
            }

            int totalPercent = currentPercent + todoItem.Progressions?.Sum(p => p.Percent) ?? 0;

            if (currentPercent > Constants.Constants.Validations.TodoItem.Progression.MAX_PERCENT)
            {
                throw new ArgumentException($"Total progress percentage ({totalPercent}) cannot exceed 100.");
            }
        }
    }
}
