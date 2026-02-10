using FastEndpoints;
using FastEndpoints.Swagger;
using CMS.Modules.Ticketing;
using CMS.Modules.Submission;
using CMS.Modules.Registration;

var builder = WebApplication.CreateBuilder();

builder.Services.AddRegistrationModule(builder.Configuration);
builder.Services.AddSubmissionModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);

builder.Services
   .AddFastEndpoints()
   .SwaggerDocument();

var app = builder.Build();
app.UseFastEndpoints()
   .UseSwaggerGen();
app.Run();
