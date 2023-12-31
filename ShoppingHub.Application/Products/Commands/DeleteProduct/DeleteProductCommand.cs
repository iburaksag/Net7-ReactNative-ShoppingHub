using System;
using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.Products.Commands.DeleteProduct
{
    public sealed record DeleteProductCommand(int Id) : IRequest<Unit>;
}

