using TodoList.Application.Enums;
using TodoList.Application.TodoList.Validators.Contracts;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Queries;

namespace TodoList.Application.TodoList.Validators
{
    internal class CreateTodoItemValidator : ITodoListValidator
    {
        private readonly ITodoListQueriesRepository _todoListQueriesRepository;

        public CreateTodoItemValidator(ITodoListQueriesRepository todoListQueriesRepository)
        {
            _todoListQueriesRepository = todoListQueriesRepository;
        }

        bool ITodoListValidator.IsApplicable(TodoListActions action)
        {
            return action == TodoListActions.Create;
        }

        void ITodoListValidator.Validate(TodoItem todoItem)
        {
            if (todoItem.Id <= 0)
            {
                throw new ArgumentException($"{nameof(todoItem.Id)} must be a positive integer.");
            }

            if (string.IsNullOrWhiteSpace(todoItem.Title))
            {
                throw new ArgumentException($"{nameof(todoItem.Title)} cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(todoItem.Description))
            {
                throw new ArgumentException($"{nameof(todoItem.Description)} cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(todoItem.Category))
            {
                throw new ArgumentException($"{nameof(todoItem.Category)} cannot be empty.");
            }
            else if (_todoListQueriesRepository.GetAllCategoriesAsync().Result.All(c => c != todoItem.Category))
            {
                throw new ArgumentException($"{nameof(todoItem.Category)} does not exist.");
            }
        }
    }
}
