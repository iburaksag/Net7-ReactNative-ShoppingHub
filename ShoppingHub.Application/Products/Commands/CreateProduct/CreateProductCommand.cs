using System;
using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.Products.Commands.CreateProduct
{
	public sealed record CreateProductCommand(
                int Id,
                string ProductName,
                string ProductCode,
                decimal UnitPrice,
                string Category,
                string Description) : IRequest<ProductDto>;
}

