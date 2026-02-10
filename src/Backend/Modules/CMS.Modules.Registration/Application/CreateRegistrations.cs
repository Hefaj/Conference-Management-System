using CMS.Modules.Registration.Domain.Models;
using CMS.Modules.Registration.Domain.Repositories;
using CMS.Shared.DDD.Models;
using FastEndpoints;
using FluentValidation;
using System.Net.Mail;

namespace CMS.Modules.Registration.Application;

public record CreateRegistrationsRequest
{
    public OrderLineType Type { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

public class CreateUserValidator : Validator<CreateRegistrationsRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
    }
}

public class CreateRegistrations : Endpoint<CreateRegistrationsRequest, Guid>
{
    private readonly IAttendeeRepository _attendeeRepository;

    public CreateRegistrations(IAttendeeRepository attendeeRepository)
    {
        _attendeeRepository = attendeeRepository;
    }

    public override void Configure()
    {
        Post("/api/registration/registrations");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateRegistrationsRequest req, CancellationToken ct)
    {
        var personName = PersonName.Create(req.FirstName, req.LastName);
        if (personName.IsFailure)
        {
            personName.Errors.ToList().ForEach(x => AddError(x));
        }
        var emailAddress = EmailAddress.Create(req.Email);
        if (emailAddress.IsFailure)
        {
            emailAddress.Errors.ToList().ForEach(x => AddError(x));
        }

        ThrowIfAnyErrors();

        var member = Member.Create(personName.Value, emailAddress.Value);
        if (member.IsFailure)
        {
            member.Errors.ToList().ForEach(x => AddError(x));
            ThrowIfAnyErrors();
        }

        var attendeeResult = Attendee.Create(member.Value);
        ThrowIfAnyErrors();

        await Send.OkAsync(attendeeResult.Value.Id);
    }
}
