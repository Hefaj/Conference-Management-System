using CMS.Shared.DDD;

namespace CMS.Modules.Cfp.Domain.Events;

internal record ConferenceAcceptedEvent(Guid Id) : IDomainEvent;