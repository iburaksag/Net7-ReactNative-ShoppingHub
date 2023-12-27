using System;
using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.Products.Queries.GetProductById
{
	public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
}

