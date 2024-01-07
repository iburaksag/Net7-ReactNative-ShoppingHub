namespace ShoppingHub.Application.DTO
{
	public record ProductDto(
					int Id,
                    string ProductName,
					string ProductCode,
					double UnitPrice,
					string Category,
					string? Description);
}

