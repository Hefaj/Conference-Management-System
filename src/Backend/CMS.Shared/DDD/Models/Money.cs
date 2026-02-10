using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Shared.DDD.Models;

public class Money : ValueObject
{
    public double Value { get; init; }
    public string Currency { get; init; }

    private Money(double value, string currency) 
    {
        Value = value;
        Currency = currency;
    }

    public static Result<Money> Create(double value, string currency)
    {
        var errors = new List<string>();

        if (value < 0)
        {
            errors.Add("Value cannot be negative.");
        }
        if (string.IsNullOrWhiteSpace(currency))
        {
            errors.Add("Currency cannot be empty.");
        }

        if (errors.Count != 0)
        {
            return Result<Money>.Failure(errors);
        }

        return Result<Money>.Success(new Money(value, currency));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
        yield return Currency;
    }

}
