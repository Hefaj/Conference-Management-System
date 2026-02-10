using CMS.Shared.DDD;

namespace CMS.Modules.Registration.Domain.Events;

internal record AttendeeCreatedEvent(Guid Id) : IDomainEvent;
