namespace ShoppingHub.Application.DTO
{
	public record RegisterDto(
                   string UserName,
                   string Password,
                   string Email,
                   string FirstName,
                   string LastName,
                   string PhoneNumber);
}

