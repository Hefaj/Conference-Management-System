using CMS.Shared.DDD;

namespace CMS.Modules.Cfp.Domain.Events;

internal record ConferenceCreatedEvent(Guid Id) : IDomainEvent;
