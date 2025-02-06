namespace Domain.Core.Common.Models;

public abstract class ValueObject
{
    protected bool Equals(ValueObject other)
    {
        throw new NotImplementedException();
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj) || obj.GetType() != GetType()) return false;
        var other = (ValueObject)obj;
        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
    
    public static bool operator ==(ValueObject? left, ValueObject? right) => Equals(left, right);

    public static bool operator !=(ValueObject? left, ValueObject? right) => !Equals(left, right);
}