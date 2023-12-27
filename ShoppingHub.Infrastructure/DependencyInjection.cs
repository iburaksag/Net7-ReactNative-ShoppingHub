using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShoppingHub.Application.Abstractions.Services;
using ShoppingHub.Application.DTO;
using ShoppingHub.Application.Products.Commands.CreateProduct;
using ShoppingHub.Application.Validations;
using ShoppingHub.Application.Validations.DTO;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;
using ShoppingHub.Infrastructure.Repositories;
using ShoppingHub.Infrastructure.Repositories.Common;
using ShoppingHub.Infrastructure.Services;

namespace ShoppingHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketDetailRepository, BasketDetailRepository>();

            // Register validators
            services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>();
            services.AddTransient<IValidator<User>, UserValidator>();
            services.AddTransient<IValidator<BasketDetail>, BasketDetailValidator>();
            services.AddTransient<IValidator<Basket>, BasketValidator>();

            services.AddTransient<IValidator<CreateProductCommand>, CreateProductCommandValidator>();

            //Register services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    };
}

