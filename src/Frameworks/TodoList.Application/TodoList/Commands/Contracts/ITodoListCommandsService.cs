namespace TodoList.Application.TodoList.Commands.Contracts
{
    public interface ITodoListCommandsService
    {
        void AddItem(int id, string title, string description, string category);
        void UpdateItem(int id, string description);
        void RemoveItem(int id);
        void RegisterProgression(int id, DateTime dateTime, decimal percent);
    }
}
