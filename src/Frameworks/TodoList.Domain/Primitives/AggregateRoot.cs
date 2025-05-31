namespace TodoList.Domain.Primitives
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot(int id) : base(id)
        {
        }
    }
}
