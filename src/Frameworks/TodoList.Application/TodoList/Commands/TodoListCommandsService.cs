using TodoList.Application.TodoList.Commands.Contracts;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Commands;
using TodoList.Domain.IRepositories.TodoList.Queries;
using TodoList.Infrastructure.Repositories.TodoList.Queries;

namespace TodoList.Application.TodoList.Commands
{
    internal class TodoListCommandsService : ITodoListCommandsService
    {
        private readonly ITodoItemCommandsRepository _todoItemCommandsRepository;
        private readonly IProgressionCommandsRepository _progressionCommandsRepository;
        private readonly ITodoListQueriesRepository _todoListQueriesRepository;

        public TodoListCommandsService(ITodoItemCommandsRepository todoItemCommandsRepository, 
            IProgressionCommandsRepository progressionCommandsRepository,
            ITodoListQueriesRepository todoListQueriesRepository)
        {
            _todoItemCommandsRepository = todoItemCommandsRepository;
            _progressionCommandsRepository = progressionCommandsRepository;
            _todoListQueriesRepository = todoListQueriesRepository;
        }

        public void AddItem(int id, string title, string description, string category)
        {
            _todoItemCommandsRepository.Create(new TodoItem
            {
                Id = id,
                Title = title,
                Description = description,
                Category = category
            });
        }

        public void UpdateItem(int id, string description)
        {
            _todoItemCommandsRepository.Update(new TodoItem
            {
                Id = id,
                Description = description,
            });
        }

        public void RemoveItem(int id)
        {
            _todoItemCommandsRepository.Remove(id);
        }

        public void RegisterProgression(int id, DateTime dateTime, int percent)
        {
            _progressionCommandsRepository.Create(new Progression
            {
                TodoItemId = id,
                Date = dateTime,
                Percent = percent
            });
        }
    }
}
