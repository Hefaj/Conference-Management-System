using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Shared.DDD.Models;

public class DateTimeRange : ValueObject
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }

    private DateTimeRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }

    public static Result<DateTimeRange> Create(DateTime start, DateTime end)
    {
        if (start > end)
        {
            return Result<DateTimeRange>.Failure("Start date must be before end date.");
        }
        return Result<DateTimeRange>.Success(new DateTimeRange(start, end));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Start;
        yield return End;
    }
}
