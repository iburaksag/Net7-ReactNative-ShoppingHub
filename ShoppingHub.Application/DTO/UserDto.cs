using System;
namespace ShoppingHub.Application.DTO
{
	public record UserDto(
		int Id,
		string Username,
		string Email,
		string FirstName,
		string LastName,
		string PhoneNumber);
}

