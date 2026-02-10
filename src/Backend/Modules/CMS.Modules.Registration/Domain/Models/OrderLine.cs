using CMS.Shared.DDD;

namespace CMS.Modules.Registration.Domain.Models;

internal class OrderLine : ValueObject
{
    public OrderLineType Type { get; private set; }
    public int Count { get; private set; }

    private OrderLine(OrderLineType type, int count)
    {
        Type = type;
        Count = count;
    }

    public static Result<OrderLine> Create(OrderLineType type, int count)
    {
        if (count <= 0) return Result<OrderLine>.Failure("Count must be greater than zero");
        return Result<OrderLine>.Success(new OrderLine(type, count));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Type;
        yield return Count;
    }
}
