using CMS.Shared.DDD;

namespace CMS.Modules.Registration.Domain.Events;

internal record AttendeeAcceptedEvent(Guid Id) : IDomainEvent;