using CMS.Shared.DDD;

namespace CMS.Modules.Submission.Domain.Events;

internal record AbstractAcceptedEvent(Guid Id) : IDomainEvent;