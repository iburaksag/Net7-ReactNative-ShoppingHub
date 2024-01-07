using System.Security.Cryptography;
using System.Text;
using MediatR;
using ShoppingHub.Domain;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, AuthResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateUserCommandValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return new AuthResult { Success = false, Message = "Invalid email or password", Errors = errors };
            }

            if (await _userRepository.IsUsernameTakenAsync(request.registerDto.UserName))
            {
                return new AuthResult { Success = false, Message = "Username already exists" };
            }

            if (await _userRepository.IsEmailTakenAsync(request.registerDto.Email))
            {
                return new AuthResult { Success = false, Message = "Email already exists" };
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(request.registerDto.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                UserName = request.registerDto.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = request.registerDto.Email,
                FirstName = request.registerDto.FirstName,
                LastName = request.registerDto.LastName,
                PhoneNumber = request.registerDto.PhoneNumber
            };

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new AuthResult { Success = true, User = user };
        }

        //Hashing
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }

}

