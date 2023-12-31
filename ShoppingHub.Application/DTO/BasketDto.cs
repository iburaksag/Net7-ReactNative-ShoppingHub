using System;
using ShoppingHub.Domain.Enums;

namespace ShoppingHub.Application.DTO
{
    public record BasketDto(
                    int Id,
                    string IPAddress,
                    string OrderAddress,
                    DateTime OrderDate,
                    double OrderTotal,
                    Status Status);
}

