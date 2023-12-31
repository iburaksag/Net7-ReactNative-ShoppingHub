using System;
using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.Baskets.Commands.UpdateBasket
{
	public sealed record UpdateBasketCommand(
					int BasketId,
					string OrderAddress,
					double OrderTotal) : IRequest<BasketDto>;
}

