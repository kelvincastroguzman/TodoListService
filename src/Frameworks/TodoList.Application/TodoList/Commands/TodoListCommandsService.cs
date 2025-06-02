using TodoList.Application.TodoList.Commands.Contracts;
using TodoList.Application.TodoList.Validators.Contracts;
using TodoList.Domain.Entities;
using TodoList.Domain.IRepositories.TodoList.Commands;
using TodoList.Domain.IRepositories.TodoList.Queries;

namespace TodoList.Application.TodoList.Commands
{
    internal class TodoListCommandsService : ITodoListCommandsService
    {
        private readonly ITodoItemCommandsRepository _todoItemCommandsRepository;
        private readonly IProgressionCommandsRepository _progressionCommandsRepository;
        private readonly ITodoListQueriesRepository _todoListQueriesRepository;
        private readonly IProgressionQueriesRepository _progressionQueriesRepository;
        private readonly IEnumerable<ITodoListValidator> _todoListValidators;

        public TodoListCommandsService(ITodoItemCommandsRepository todoItemCommandsRepository, 
            IProgressionCommandsRepository progressionCommandsRepository,
            ITodoListQueriesRepository todoListQueriesRepository,
            IProgressionQueriesRepository progressionQueriesRepository,
            IEnumerable<ITodoListValidator> todoListValidators)
        {
            _todoItemCommandsRepository = todoItemCommandsRepository;
            _progressionCommandsRepository = progressionCommandsRepository;
            _todoListQueriesRepository = todoListQueriesRepository;
            _progressionQueriesRepository = progressionQueriesRepository;
            _todoListValidators = todoListValidators;
        }

        void ITodoListCommandsService.AddItem(int id, string title, string description, string category)
        {
            id = _todoListQueriesRepository.GetNextIdAsync().Result;

            var todoItem = new TodoItem(id)
            {
                Id = id,
                Title = title,
                Description = description,
                Category = category
            };

            _todoListValidators
                .First(v => v.IsApplicable(Enums.TodoListActions.Create))
                .Validate(todoItem);

            _ = _todoItemCommandsRepository.CreateAsync(todoItem).Result;
        }

        void ITodoListCommandsService.UpdateItem(int id, string description)
        {
            var todoItem = new TodoItem(id)
            {
                Id = id,
                Description = description,
            };

            _todoListValidators
                .First(v => v.IsApplicable(Enums.TodoListActions.Update))
                .Validate(todoItem);

            _ = _todoItemCommandsRepository.UpdateAsync(todoItem).Result;
        }

        void ITodoListCommandsService.RemoveItem(int id)
        {
            _todoListValidators
                .First(v => v.IsApplicable(Enums.TodoListActions.Remove))
                .Validate(new TodoItem(id));

            _ = _todoItemCommandsRepository.RemoveAsync(id).Result;
        }

        void ITodoListCommandsService.RegisterProgression(int id, DateTime dateTime, int percent)
        {
            int progressionId = _progressionQueriesRepository.GetNextIdAsync().Result;
            var progression = new Progression(progressionId)
            {
                Id = progressionId,
                TodoItemId = id,
                Date = dateTime,
                Percent = percent
            };

            var todoItem = new TodoItem(id)
            {
                Id = id,
                Progressions = [progression]
            };

            _todoListValidators
                .First(v => v.IsApplicable(Enums.TodoListActions.RegisterProgression))
                .Validate(todoItem);

            _ = _progressionCommandsRepository.CreateAsync(progression).Result;
        }
    }
}
