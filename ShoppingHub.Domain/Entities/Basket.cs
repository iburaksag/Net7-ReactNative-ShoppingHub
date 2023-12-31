using ShoppingHub.Domain.Entities.Common;
using ShoppingHub.Domain.Enums;

namespace ShoppingHub.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public int UserId { get; set; }
        public string? IPAddress { get; set; }
        public string? OrderAddress { get; set; }
        public DateTime? OrderDate { get; set; }
        public double? OrderTotal { get; set; }
        public Status Status { get; set; }

        public User User { get; set; }
        public ICollection<BasketDetail>? BasketDetails { get; set; } 
    }
}

