using CMS.Shared.DDD;
using CMS.Shared.DDD.Models;

namespace CMS.Modules.Registration.Domain.Models;

internal class RegistrationOrder : RootEntity
{
    public Money TotalAmount { get; private set; }
    public OrderLine OrderLine { get; private set; }

    private RegistrationOrder(Money totalAmount, OrderLine orderLine) 
    {
        TotalAmount = totalAmount;
        OrderLine = orderLine;
    }

    public static Result<RegistrationOrder> Create(Money totalAmount, OrderLine orderLine)
    {
        return Result<RegistrationOrder>.Success(new RegistrationOrder(totalAmount, orderLine));
    }
}
