namespace CMS.Shared.DDD;

public abstract class ValueObject
{
    protected abstract IEnumerable<object> GetAtomicValues();

    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }

        return left is null || left.Equals(right!);
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !left.Equals(right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null || this.GetType() != obj.GetType())
        {
            return false;
        }

        ValueObject valueObject = (ValueObject)obj;
        return GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var obj in GetAtomicValues())
        {
            hash.Add(obj);
        }
        return hash.ToHashCode();
    }
}
