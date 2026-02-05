using FastEndpoints;
using FastEndpoints.Swagger;

using CMS.Modules.Cfp;
using CMS.Modules.Ticketing;

var builder = WebApplication.CreateBuilder();

builder.Services.AddCfpModule(builder.Configuration);
builder.Services.AddTicketingModule(builder.Configuration);

builder.Services
   .AddFastEndpoints()
   .SwaggerDocument();

var app = builder.Build();
app.UseFastEndpoints()
   .UseSwaggerGen();
app.Run();
