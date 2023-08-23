using System.Runtime.ExceptionServices;

namespace CleanArchitecture.Domain;

public abstract class Entity : IEquatable<Entity>
{
    protected Entity (Guid id)
    {
        Id = id;
    }
    public Guid Id { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        if (obj.GetType() != typeof (Entity))
            return false;
        
        if (obj is not Entity entity)
            return false;

        return entity.Id == Id;
    }

    public bool Equals(Entity? other)
    {
        if (other is null)
            return false;
        
        if (other.GetType() != typeof (Entity))
            return false;

        return other.Id == Id;
    }

    public static bool operator == (Entity? left,Entity? right)
    {
       return left is not null && right is not null && left.Equals(right);
    }

    public static bool operator != (Entity? left,Entity? right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }


}
