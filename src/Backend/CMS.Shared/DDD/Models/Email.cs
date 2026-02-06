using System.Text.RegularExpressions;

namespace CMS.Shared.DDD.Models;

public class Email : ValueObject
{
    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result<Email>.Failure("Email cannot be empty.");

        if (!EmailRegex.IsMatch(email))
            return Result<Email>.Failure("Email format is invalid.");

        return Result<Email>.Success(new Email(email));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}