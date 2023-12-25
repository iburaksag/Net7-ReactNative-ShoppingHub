using ShoppingHub.Application.DTO;
using ShoppingHub.Domain.Entities;

namespace ShoppingHub.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterDto registerDto);
        Task<AuthResult> LoginAsync(LoginDto loginDto);
        Task<bool> IsUsernameExists(string username);
        Task<bool> IsEmailExists(string email);
    }
}

