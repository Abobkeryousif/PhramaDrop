using PharmaDrop.Infrastructure.DependenctInjection;
using FluentValidation.AspNetCore;
using PharmaDrop.Aplication.DependencyInjection;
using PharmaDrop.Aplication.Validtor;
using FluentValidation;
using Microsoft.AspNetCore.RateLimiting;
using PharmaDrop.Application.Validtor;


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

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.Run();
