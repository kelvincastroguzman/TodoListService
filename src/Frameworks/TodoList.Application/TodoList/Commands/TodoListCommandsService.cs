using TodoList.Application.TodoList.Commands.Contracts;

namespace TodoList.Application.TodoList.Commands
{
    internal class TodoListCommandsService : ITodoListCommandsService
    {
        public TodoListCommandsService()
        {
        }

        public void AddItem(int id, string title, string description, string category)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(int id, string description)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(int id)
        {
            throw new NotImplementedException();
        }

        public void RegisterProgression(int id, DateTime dateTime, decimal percent)
        {
            throw new NotImplementedException();
        }
    }
}
