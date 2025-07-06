using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.RateLimiting;
using PharmaDrop.API.Middleware;
using PharmaDrop.Aplication.DependencyInjection;
using PharmaDrop.Aplication.Validtor;
using PharmaDrop.Application.Validtor;
using PharmaDrop.Core.Common;
using PharmaDrop.Infrastructure.DependenctInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.InfraRegister(builder.Configuration);

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UserDtoValidtor>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserDtoValidtor>();
builder.Services.AppRegister();
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddRateLimiter(limit=> 
{
    limit.AddFixedWindowLimiter("Fixed", option =>
    {
        option.PermitLimit = 1;
        option.Window = TimeSpan.FromSeconds(5);
        option.QueueLimit = 0;
    });
    limit.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())

{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Added empty to exception meddleware
app.UseExceptionHandler(_=> { });
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.Run();
