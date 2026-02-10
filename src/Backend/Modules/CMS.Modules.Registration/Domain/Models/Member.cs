using CMS.Shared.DDD;
using CMS.Shared.DDD.Models;

namespace CMS.Modules.Registration.Domain.Models;

public class Member : ValueObject
{
    public PersonName PersonName { get; init; }
    public EmailAddress EmailAddress { get; init; }

    private Member(PersonName personName, EmailAddress emailAddress)
    {
        PersonName = personName;
        EmailAddress = emailAddress;
    }

    public static Result<Member> Create(PersonName personName, EmailAddress emailAddress)
    {
        return Result<Member>.Success(new Member(personName, emailAddress));
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return PersonName;
        yield return EmailAddress;
    }
}
