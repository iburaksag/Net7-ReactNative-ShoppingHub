using System.Security.Cryptography;
using System.Text;
using MediatR;
using ShoppingHub.Domain;
using ShoppingHub.Domain.Repositories;

namespace ShoppingHub.Application.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResult>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new LoginUserCommandValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return new AuthResult { Success = false, Message = "Validation failed", Errors = errors };
            }

            var user = await _userRepository.GetByEmailAsync(request.loginDto.Email);

            if (user == null || !VerifyPassword(request.loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new AuthResult { Success = false, Message = "Invalid email or password" };
            }

            return new AuthResult { Success = true, User = user };
        }

        //Verify
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false; // if mismatch
                }
            }
            return true;
        }
    }
}

