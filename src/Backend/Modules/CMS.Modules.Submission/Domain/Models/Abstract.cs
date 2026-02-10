using CMS.Modules.Submission.Domain.Events;
using CMS.Shared.DDD;

namespace CMS.Modules.Submission.Domain.Models;

internal class Abstract : RootEntity
{
    public string Title { get; private set; }
    public string AbstractText { get; private set; }
    public Guid AuthorId { get; private set; }
    public AbstractStatus Status { get; private set; }
    public Attachment? Attachment { get; private set; }

    public string? InternalComment { get; private set; }
    // For EF Core
    private Abstract() { }

    private Abstract(string title, string abstractText, Guid speaker) {
        Id = Guid.NewGuid();
        Title = title;
        AbstractText = abstractText;
        AuthorId = speaker;
        Status = AbstractStatus.Submitted;

        AddDomainEvent(new AbstractCreatedEvent(Id));
    }

    public static Result<Abstract> Create(string title, string @abstract, Guid speakerId)
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


        if (erros.Count != 0)
        {
            return Result<Abstract>.Failure(erros);
        }
            
        return Result <Abstract>.Success(new Abstract(title, @abstract, speakerId));
    }

    public Result AssignAttachment(Attachment attachment)
    {
        if (Status == AbstractStatus.Accepted)
        {
            return Result.Failure("Cannot add attachment to an accepted conference.");
        }
        Attachment = attachment;
        return Result.Success();
    }

    public Result Accept()
    {
        if (Status == AbstractStatus.Rejected)
        {
            return Result.Failure("Cannot accept a rejected conference without reopening.");
        }

        Status = AbstractStatus.Accepted;
        AddDomainEvent(new AbstractAcceptedEvent(Id));

        return Result.Success();
    }

    public Result Reject(string reason)
    {
        if (Status == AbstractStatus.Accepted)
        {
            return Result.Failure("Cannot reject already accepted conference.");
        }

        Status = AbstractStatus.Rejected;
        InternalComment = reason;

        return Result.Success();
    }

    public Result Update(string title, string abstractText)
    {
        if (Status == AbstractStatus.Accepted)
        {
            return Result.Failure("Cannot update accepted conference.");
        }

        Title = title;
        AbstractText = abstractText;

        return Result.Success();
    }
}
