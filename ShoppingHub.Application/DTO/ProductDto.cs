using System;
namespace ShoppingHub.Application.DTO
{
	public record ProductDto(
                    string ProductName,
					string ProductCode,
					double UnitPrice,
					string Category,
					string? Description);
}

