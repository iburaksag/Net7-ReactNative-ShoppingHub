using ShoppingHub.Domain.Entities.Common;

namespace ShoppingHub.Domain.Entities
{
    public class BasketDetail : BaseEntity
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public Basket Basket { get; set; }
        public Product Product { get; set; }
    }
}

