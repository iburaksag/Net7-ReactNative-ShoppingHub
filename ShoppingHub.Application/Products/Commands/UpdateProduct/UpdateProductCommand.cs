using System;
using MediatR;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.Products.Commands.UpdateProduct
{
	public class UpdateProductCommand : IRequest<ProductDto>
	{
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
    }
}

