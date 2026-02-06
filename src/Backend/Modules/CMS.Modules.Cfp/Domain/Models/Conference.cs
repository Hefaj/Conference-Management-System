using CMS.Modules.Cfp.Domain.Events;
using CMS.Shared.DDD;

namespace CMS.Modules.Cfp.Domain.Models;

internal class Conference : AggregateRoot
{
    public Guid ConferenceId { get; private set; }
    public string Title { get; private set; }
    public string Abstract { get; private set; }
    public Speaker Speaker { get; private set; }
    public ConferenceStatus Status { get; private set; }

    public string? InternalComment { get; private set; }

    // For EF Core
    private Conference() { }

    private Conference(Guid conferenceId, string title, string @abstract, Speaker speaker) {
        Id = Guid.NewGuid();
        ConferenceId = conferenceId;
        Title = title;
        Abstract = @abstract;
        Speaker = speaker;
        Status = ConferenceStatus.Submitted;

        AddDomainEvent(new ConferenceCreatedEvent(Id));
    }

    public static Result<Conference> Create(Guid conferenceId, string title, string @abstract, Speaker speaker)
    {
        var erros = new List<string>();
        if (string.IsNullOrWhiteSpace(title))
        {
            erros.Add("Title cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(@abstract))
        {
            erros.Add("Abstract cannot be empty.");
        }

        if (speaker is null)
        {
            erros.Add("Speaker is required.");
        }

        if (erros.Count != 0)
        {
            return Result<Conference>.Failure(erros);
        }
            
        return Result <Conference>.Success(new Conference(conferenceId, title, @abstract, speaker!));
    }

    public Result Accept()
    {
        if (Status == ConferenceStatus.Rejected)
        {
            return Result.Failure("Cannot accept a rejected conference without reopening.");
        }

        Status = ConferenceStatus.Accepted;
        AddDomainEvent(new ConferenceAcceptedEvent(Id));

        return Result.Success();
    }

    public Result Reject(string reason)
    {
        if (Status == ConferenceStatus.Accepted)
        {
            return Result.Failure("Cannot reject already accepted conference.");
        }

        Status = ConferenceStatus.Rejected;
        InternalComment = reason;

        return Result.Success();
    }

    public Result Update(string title, string @abstract)
    {
        if (Status == ConferenceStatus.Accepted)
        {
            return Result.Failure("Cannot update accepted conference.");
        }

        Title = title;
        Abstract = @abstract;

        return Result.Success();
    }
}
