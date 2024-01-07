using System.ComponentModel.DataAnnotations;

namespace ShoppingHub.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public BaseEntity()
        {
            this.CreatedAt = DateTime.UtcNow;
        }
    }
}

