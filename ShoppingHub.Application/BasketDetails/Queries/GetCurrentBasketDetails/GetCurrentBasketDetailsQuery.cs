using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.BasketDetails.Queries.GetCurrentBasketDetails
{
	public sealed record GetCurrentBasketDetailsQuery(int BasketId): IRequest<List<BasketDetailDto>>;
}

