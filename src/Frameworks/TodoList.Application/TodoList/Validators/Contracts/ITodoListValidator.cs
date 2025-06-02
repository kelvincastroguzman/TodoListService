using TodoList.Application.Enums;
using TodoList.Domain.Entities;

namespace TodoList.Application.TodoList.Validators.Contracts
{
    public interface ITodoListValidator
    {
        bool IsApplicable(TodoListActions action);
        void Validate(TodoItem todoItem);
    }
}
