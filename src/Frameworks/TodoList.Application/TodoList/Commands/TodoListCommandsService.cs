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
        //private readonly ITodoListValidator[] _todoListValidators;

        public TodoListCommandsService(ITodoItemCommandsRepository todoItemCommandsRepository, 
            IProgressionCommandsRepository progressionCommandsRepository,
            ITodoListQueriesRepository todoListQueriesRepository 
            )
        //ITodoListValidator[] todoListValidators
        {
            _todoItemCommandsRepository = todoItemCommandsRepository;
            _progressionCommandsRepository = progressionCommandsRepository;
            _todoListQueriesRepository = todoListQueriesRepository;
            //_todoListValidators = todoListValidators;
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

            //_todoListValidators
            //    .Where(v => v.IsApplicable(Enums.TodoListActions.Create))
            //    .First()
            //    .Validate(todoItem);

            _todoItemCommandsRepository.Create(todoItem);
        }

        void ITodoListCommandsService.UpdateItem(int id, string description)
        {
            var todoItem = new TodoItem(id)
            {
                Id = id,
                Description = description,
            };

            //_todoListValidators
            //    .Where(v => v.IsApplicable(Enums.TodoListActions.Update))
            //    .First()
            //    .Validate(todoItem);

            _todoItemCommandsRepository.Update(todoItem);
        }

        void ITodoListCommandsService.RemoveItem(int id)
        {
            //_todoListValidators
            //    .Where(v => v.IsApplicable(Enums.TodoListActions.Remove))
            //    .First()
            //    .Validate(new TodoItem(id) { Id = id });

            _todoItemCommandsRepository.Remove(id);
        }

        void ITodoListCommandsService.RegisterProgression(int id, DateTime dateTime, int percent)
        {
            var todoItem = new TodoItem(id)
            {
                Id = id,
                Progressions = new List<Progression>
                {
                    new Progression(id)
                    {
                        Id = id,
                        TodoItemId = id,
                        Date = dateTime,
                        Percent = percent
                    }
                }
            };

            //_todoListValidators
            //    .Where(v => v.IsApplicable(Enums.TodoListActions.RegisterProgression))
            //    .First()
            //    .Validate(todoItem);

            _progressionCommandsRepository.Create(todoItem.Progressions.First());
        }
    }
}
