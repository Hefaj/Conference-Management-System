using CMS.Shared.DDD;
using CMS.Shared.DDD.Models;

namespace CMS.Modules.Cfp.Domain.Models;

internal class Speaker : ValueObject
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string Bio { get; private set; }
    private Speaker(string name, Email email, string? bio)
    {
        Name = name;
        Email = email;
        Bio = bio ?? string.Empty;
    }

    public static Result<Speaker> Create(string name, string email, string? bio)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add("Name cannot be empty.");
        }

        var emailResult = Email.Create(email);
        if (emailResult.IsFailure) {
            errors.AddRange(emailResult.Errors);
        }

        if (errors.Count != 0) {
            return Result<Speaker>.Failure(errors);
        }

        return Result<Speaker>.Success(new Speaker(name, emailResult.Value, bio));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return Email;
        yield return Bio;
    }
}
