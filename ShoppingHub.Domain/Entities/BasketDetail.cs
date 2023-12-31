using ShoppingHub.Domain.Entities.Common;
using ShoppingHub.Domain.Enums;

namespace ShoppingHub.Domain.Entities
{
    public class BasketDetail : BaseEntity
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Basket? Basket { get; set; }
        public Product? Product { get; set; }
    }
}
