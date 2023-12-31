using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.BasketDetails.Commands.CreateBasketDetail
{
    public sealed record CreateBasketDetailCommand(
        int BasketId,
        int ProductId,
        int Quantity) : IRequest<BasketDetailDto>;
}

