using ShoppingHub.Domain.Entities;

namespace ShoppingHub.Application
{
	public class AuthResult
	{
        public bool Success { get; set; }
        public string? Message { get; set; }
        public User User { get; set; }
        public List<string>? Errors { get; set; }
    }
}

