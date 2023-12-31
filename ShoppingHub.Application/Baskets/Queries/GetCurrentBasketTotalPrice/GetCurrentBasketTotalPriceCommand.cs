using MediatR;

namespace ShoppingHub.Application.Baskets.Queries.GetBasketTotalPrice
{
	public sealed record GetCurrentBasketTotalPriceCommand(int BasketId) : IRequest<double>;
}

