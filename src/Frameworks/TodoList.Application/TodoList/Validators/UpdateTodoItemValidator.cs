using TodoList.Application.Enums;
using TodoList.Application.TodoList.Validators.Contracts;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Queries;

namespace TodoList.Application.TodoList.Validators
{
    internal class UpdateTodoItemValidator : ITodoListValidator
    {
        private readonly IProgressionQueriesRepository _progressionQueriesRepository;

        public UpdateTodoItemValidator(IProgressionQueriesRepository progressionQueriesRepository)
        {
            _progressionQueriesRepository = progressionQueriesRepository;
        }

        bool ITodoListValidator.IsApplicable(TodoListActions action)
        {
            return action == TodoListActions.Update;
        }

        void ITodoListValidator.Validate(TodoItem todoItem)
        {
            if (todoItem.Id <= 0)
            {
                throw new ArgumentException("Id must be a positive integer.", nameof(todoItem.Id));
            }

            if (string.IsNullOrWhiteSpace(todoItem.Description))
            {
                throw new ArgumentException("Description cannot be empty.", nameof(todoItem.Description));
            }

            var existingProgressions = _progressionQueriesRepository.GetByTodoItemIdAsync(todoItem.Id).Result;
            int totalPercent = existingProgressions?.Sum(p => p.Percent) ?? 0;

            if (totalPercent > Constants.Constants.Validations.TodoItem.Progression.PERCENT_NOT_ALLOWED_FOR_MODIFICATIONS)
            {
                throw new ArgumentException("Todo-item cannot be updated. The current percentage is higher than 50.", nameof(totalPercent));
            }
        }
    }
}
