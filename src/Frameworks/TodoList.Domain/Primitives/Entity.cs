namespace TodoList.Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        public int Id { get; private init; }

        protected Entity(int id)
        {
            Id = id;
        }

        public static bool operator ==(Entity? first, Entity? second)
        {
            return first is not null && second is not null && first.Equals(second);
        }

        public static bool operator !=(Entity? first, Entity? second)
        {
            return !(first == second);
        }

        public bool Equals(Entity? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Entity)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
