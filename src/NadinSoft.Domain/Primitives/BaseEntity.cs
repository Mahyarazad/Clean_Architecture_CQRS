namespace NadinSoft.Domain.Primitives
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        // It is useful when we want to create an instance with a specified ID.
        protected BaseEntity(Guid id)
        {
            Id = id;
        }

        // Allow Efcore to generate unique ID for Id
        protected BaseEntity() { }

        public Guid Id { get; private set; }
        public override bool Equals(object? obj)
        {
            if(obj is null)
            {
                return false;
            }

            if(obj.GetType() != GetType())
            {
                return false;
            }

            if(obj is not BaseEntity entity)
            {
                return false;
            }

            return entity.Id == Id;
        }

        public bool Equals(BaseEntity? other)
        {
            if(other is null)
            {
                return false;
            }

            return other.Id == Id;
        }

        public static bool operator ==(BaseEntity? first, BaseEntity? second)
        {
            return first is not null && second is not null && first.Equals(second);
        }
        public static bool operator !=(BaseEntity? first, BaseEntity? second)
        {
            return !(first == second);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 50;
        }
    }
}
