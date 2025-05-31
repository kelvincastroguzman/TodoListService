using TodoList.Application.Enums;
using TodoList.Application.TodoList.Validators.Contracts;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Queries;
using static TodoList.Application.Constants.Constants.Validations.TodoItem;

namespace TodoList.Application.TodoList.Validators
{
    internal class RegisterProgressionTodoItemValidator : ITodoListValidator
    {
        private readonly IProgressionQueriesRepository _progressionQueriesRepository;

        public RegisterProgressionTodoItemValidator(IProgressionQueriesRepository progressionQueriesRepository)
        {
            _progressionQueriesRepository = progressionQueriesRepository;
        }

        public bool IsApplicable(TodoListActions action)
        {
            return action == TodoListActions.RegisterProgression;
        }

        public void Validate(TodoItem todoItem)
        {
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

            if (totalPercent > Constants.Constants.Validations.TodoItem.Progression.PERCENT_NOT_ALLOWED_FOR_MODIFICATIONS)
            {
                throw new ArgumentException("Todo-item cannot be updated. The current percentage is higher than 50.", nameof(totalPercent));
            }

            if (totalPercent + todoItem.Progressions?.Sum(p => p.Percent) > Constants.Constants.Validations.TodoItem.Progression.MAX_PERCENT)
            {
                throw new ArgumentException("Total progress percentage cannot exceed 100.", nameof(todoItem.Progressions));
            }
        }
    }
}
