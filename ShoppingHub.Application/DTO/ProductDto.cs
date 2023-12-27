using System;
namespace ShoppingHub.Application.DTO
{
	public record ProductDto(
                    int Id,
                    string ProductName,
					string ProductCode,
					decimal UnitPrice,
					string Category,
					string Description);
}

