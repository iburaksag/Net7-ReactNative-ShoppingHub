using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ShoppingHub.Application.Baskets.Commands.CreateBasket;
using ShoppingHub.Application.Products.Commands.CreateProduct;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;
using ShoppingHub.Infrastructure.Repositories;
using ShoppingHub.Infrastructure.Repositories.Common;

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
            services.AddTransient<IValidator<CreateProductCommand>, CreateProductCommandValidator>();

            //Register services
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    };
}

