using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.Products.Queries.GetAllProducts
{
	public sealed record GetAllProductsQuery() : IRequest<List<ProductDto>>;
}

