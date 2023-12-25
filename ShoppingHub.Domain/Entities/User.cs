using System.ComponentModel.DataAnnotations;
using ShoppingHub.Domain.Entities.Common;

namespace ShoppingHub.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }

        public ICollection<Basket> Baskets { get; set; }
    }
}

