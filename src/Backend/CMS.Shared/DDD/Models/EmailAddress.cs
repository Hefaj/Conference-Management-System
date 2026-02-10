using System.Text.RegularExpressions;

namespace CMS.Shared.DDD.Models;

public class EmailAddress : ValueObject
{
    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; init; }

    private EmailAddress(string value)
    {
        Value = value;
    }

    public static Result<EmailAddress> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result<EmailAddress>.Failure("Email cannot be empty.");

        if (!EmailRegex.IsMatch(email))
            return Result<EmailAddress>.Failure("Email format is invalid.");

        return Result<EmailAddress>.Success(new EmailAddress(email));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}