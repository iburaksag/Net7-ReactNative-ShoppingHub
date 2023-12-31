using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.Baskets.Queries.GetBasketsByUserId
{
    public sealed record GetBasketsByUserIdQuery(int UserId) : IRequest<List<BasketDto>>;
}

