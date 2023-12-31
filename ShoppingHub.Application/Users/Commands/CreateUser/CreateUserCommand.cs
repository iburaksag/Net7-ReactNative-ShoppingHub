using MediatR;
using ShoppingHub.Application.DTO;
using ShoppingHub.Domain;

namespace ShoppingHub.Application.Users.Commands.CreateUser
{
	public record CreateUserCommand(RegisterDto registerDto) : IRequest<AuthResult>;
}

