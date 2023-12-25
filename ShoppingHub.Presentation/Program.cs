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
builder.Services.AddApplication().AddInfrastructure();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketDetailRepository, BasketDetailRepository>();
// Register validators
builder.Services.AddTransient<IValidator<User>, UserValidator>();
builder.Services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();
builder.Services.AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>();
builder.Services.AddTransient<IValidator<Product>, ProductValidator>();
builder.Services.AddTransient<IValidator<BasketDetail>, BasketDetailValidator>();
builder.Services.AddTransient<IValidator<Basket>, BasketValidator>();
//Register services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

