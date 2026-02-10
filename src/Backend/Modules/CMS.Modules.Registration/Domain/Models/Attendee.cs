using CMS.Modules.Registration.Domain.Events;
using CMS.Shared.DDD;

namespace CMS.Modules.Registration.Domain.Models;

public class Attendee : RootEntity
{
    public AttendeeStatus Status { get; private set; }
    public Member Member { get; private set; }

    // For EF Core
    private Attendee() { }

    private Attendee(Member member) {
        Id = Guid.NewGuid();
        Member = member;
        Status = AttendeeStatus.Registered;
        AddDomainEvent(new AttendeeCreatedEvent(Id));
    }

    public static Result<Attendee> Create(Member member)
    {
        return Result<Attendee>.Success(new Attendee(member));
    }
}
