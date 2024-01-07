namespace ShoppingHub.Application.DTO
{
    public record BasketDetailDto(
                    int Id,
                    int BasketId,
                    int ProductId,
                    int Quantity,
                    DateTime CreatedAt);
}

