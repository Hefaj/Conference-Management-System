using CMS.Shared.DDD;
using CMS.Shared.DDD.Models;

namespace CMS.Modules.Submission.Domain.Models;

internal class Author : Entity
{
    public string Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public string Bio { get; private set; }
    private Author(string name, EmailAddress email, string? bio)
    {
        Name = name;
        Email = email;
        Bio = bio ?? string.Empty;
    }

    public static Result<Author> Create(string name, EmailAddress emailAddress, string? bio)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add("Name cannot be empty.");
        }

        if (errors.Count != 0) {
            return Result<Author>.Failure(errors);
        }

        return Result<Author>.Success(new Author(name, emailAddress, bio));
    }
}
