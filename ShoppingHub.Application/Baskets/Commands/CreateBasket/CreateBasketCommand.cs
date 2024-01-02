using MediatR;

namespace ShoppingHub.Application.Baskets.Commands.CreateBasket
{
    public sealed record CreateBasketCommand(int UserId) : IRequest<int>;
}

