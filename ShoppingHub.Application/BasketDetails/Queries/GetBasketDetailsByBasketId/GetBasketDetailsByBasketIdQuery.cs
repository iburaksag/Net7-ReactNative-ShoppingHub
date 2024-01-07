using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.BasketDetails.Queries.GetBasketDetailsByBasketId
{
	public record GetBasketDetailsByBasketIdQuery(int BasketId) : IRequest<List<BasketDetailDto>>;
}

