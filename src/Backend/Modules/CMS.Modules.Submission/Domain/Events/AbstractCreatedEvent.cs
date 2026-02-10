using CMS.Shared.DDD;

namespace CMS.Modules.Submission.Domain.Events;

internal record AbstractCreatedEvent(Guid Id) : IDomainEvent;
