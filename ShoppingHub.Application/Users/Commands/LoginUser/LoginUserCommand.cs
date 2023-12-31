using MediatR;
using ShoppingHub.Application.DTO;
using ShoppingHub.Domain;

namespace ShoppingHub.Application.Users.Commands.LoginUser
{
    public record LoginUserCommand(LoginDto loginDto) : IRequest<AuthResult>;
}

