using Microsoft.EntityFrameworkCore;
using ShoppingHub.Infrastructure.Data;
using ShoppingHub.Application;
using ShoppingHub.Infrastructure;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Infrastructure.Repositories;
using FluentValidation;
using ShoppingHub.Application.Validations;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Application.DTO;
using ShoppingHub.Application.Validations.DTO;
using ShoppingHub.Application.Abstractions.Services;
using ShoppingHub.Domain.Repositories.Common;
using ShoppingHub.Infrastructure.Repositories.Common;
using ShoppingHub.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI
builder.Services.AddApplication().AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

