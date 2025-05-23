using DeskBooking.Domain.DatabaseConnection;
using static DeskBooking.Domain.DatabaseConnection.DataSeeding.DataContextSeeding;
using DeskBooking.BL.Behaviours;
using Microsoft.EntityFrameworkCore;
using System;
using DeskBooking.Domain.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddMediatr();

builder.Services.AddMapper();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
var scope = app.Services.CreateScope();

await SeedBasicDataAsync(scope.ServiceProvider.GetRequiredService<DataContext>());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
