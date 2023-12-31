using System;
using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.Products.Commands.CreateProduct
{
	public sealed record CreateProductCommand(
                string ProductName,
                string ProductCode,
                double UnitPrice,
                string Category,
                string Description) : IRequest<ProductDto>;
}

