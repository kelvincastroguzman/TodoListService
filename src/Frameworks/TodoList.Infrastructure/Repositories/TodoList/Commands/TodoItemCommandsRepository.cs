using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Commands;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure.Repositories.TodoList.Commands
{
    public class TodoItemCommandsRepository : ITodoItemCommandsRepository
    {
        private readonly DbSet<TodoItem> _todoItems;

        public TodoItemCommandsRepository(TodoListDbContext dbContext)
        {
            _todoItems = dbContext.Set<TodoItem>();
        }

        public void Create(TodoItem todoItem)
        {
            _todoItems.Add(todoItem);
        }

        public void Update(TodoItem todoItem)
        {
            TodoItem? todoItemResponse = _todoItems.Find(todoItem.Id);
            if (todoItemResponse != null)
            {
                todoItemResponse.Description = todoItem.Description;
                _todoItems.Update(todoItemResponse);
            }
        }

        public void Remove(int id)
        {
            TodoItem? todoItem = _todoItems.Find(id);
            if (todoItem != null)
            {
                _todoItems.Remove(todoItem);
            }
        }
    }
}
