using DeskBooking.Domain.DatabaseConnection;
using static DeskBooking.Domain.DatabaseConnection.DataSeeding.DataContextSeeding;
using DeskBooking.BL.Behaviours;
using Microsoft.EntityFrameworkCore;
using System;
using DeskBooking.Domain.DTOs;
using DeskBooking.BL.CustomMiddlewares;
using Hangfire;
using Newtonsoft.Json;
using Hangfire.PostgreSql;
using DeskBooking.BL.Services.Abstraction;
using DeskBooking.BL.Services.Realization;
using MediatR;
using FluentValidation;
using DeskBooking.BL.Behaviours.Auth.Register;
using DeskBooking.Domain.Common.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped<IGroqAIService, GroqAIService>();
builder.Services.AddScoped<IReservationJobService, ReservationJobService>();

var groqSettings = builder.Configuration
        .GetSection("AppSettings:GroqAI")
        .Get<GroqSettings>();
builder.Services.AddSingleton(groqSettings);

builder.Services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfire((sp, config) =>
{
    config.UsePostgreSqlStorage(opts =>
    {
        opts.UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    config.UseSerializerSettings(new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
}
);
builder.Services.AddHangfireServer();

builder.Services.AddControllers();

builder.Services.AddMediatr();

builder.Services.AddMapper();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();
var scope = app.Services.CreateScope();

await SeedBasicDataAsync(scope.ServiceProvider.GetRequiredService<DataContext>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
