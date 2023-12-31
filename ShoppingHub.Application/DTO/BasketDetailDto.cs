using ShoppingHub.Domain.Enums;

namespace ShoppingHub.Application.DTO
{
    public record BasketDetailDto(
                    int BasketId,
                    int ProductId,
                    int Quantity,
                    Status Status,
                    DateTime CreatedAt);
}

