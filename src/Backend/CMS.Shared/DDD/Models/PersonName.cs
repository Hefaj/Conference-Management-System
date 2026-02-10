using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Shared.DDD.Models;

public class PersonName : ValueObject
{
    public string FirstName { get; init; }
    public string LastName { get; init; }

    private PersonName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static Result<PersonName> Create(string firstName, string lastName)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(firstName))
        {
            errors.Add("First name cannot be empty.");
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            errors.Add("Last name cannot be empty.");
        }
        if (errors.Count != 0)
        {
            return Result<PersonName>.Failure(errors);
        }
        return Result<PersonName>.Success(new PersonName(firstName, lastName));
    }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return FirstName;
        yield return LastName;
    }
}
