using MediatR;

namespace ShoppingHub.Application.BasketDetails.Commands.DeleteBasketDetail
{
    public sealed record DeleteBasketDetailCommand(int Id) : IRequest<bool>;
}

