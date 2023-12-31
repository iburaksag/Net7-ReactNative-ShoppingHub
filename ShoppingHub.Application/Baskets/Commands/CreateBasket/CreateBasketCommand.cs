using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.Baskets.Commands.CreateBasket
{
    public sealed record CreateBasketCommand(int UserId) : IRequest<int>;
}

