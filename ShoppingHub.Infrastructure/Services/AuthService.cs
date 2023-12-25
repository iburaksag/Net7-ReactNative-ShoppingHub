using System;
using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using ShoppingHub.Application;
using ShoppingHub.Application.Abstractions.Services;
using ShoppingHub.Application.DTO;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Infrastructure.Services
{
	public class AuthService : IAuthService
	{
        private readonly IUserRepository _userRepository;
        private readonly IValidator<LoginDto> _loginDtoValidator;
        private readonly IValidator<RegisterDto> _registerDtoValidator;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUserRepository userRepository, IValidator<LoginDto> loginDtoValidator, IValidator<RegisterDto> registerDtoValidator, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _loginDtoValidator = loginDtoValidator;
            _registerDtoValidator = registerDtoValidator;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResult> LoginAsync(LoginDto loginDto)
        {
            var validationResult = _loginDtoValidator.Validate(loginDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return new AuthResult { Success = false, Message = "Validation failed", Errors = errors };
            }

            var user = await _userRepository.GetByUsernameAsync(loginDto.UserName);

            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new AuthResult { Success = false, Message = "Invalid username or password" };
            }

            return new AuthResult { Success = true, User = user };
        }

        public async Task<AuthResult> RegisterAsync(RegisterDto registerDto)
        {
            var validationResult = _registerDtoValidator.Validate(registerDto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return new AuthResult { Success = false, Message = "Validation failed", Errors = errors };
            }

            if (await _userRepository.IsUsernameTakenAsync(registerDto.UserName))
            {
                return new AuthResult { Success = false, Message = "Username already exists" };
            }

            if (await _userRepository.IsEmailTakenAsync(registerDto.Email))
            {
                return new AuthResult { Success = false, Message = "Email already exists" };
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                UserName = registerDto.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber
            };

            await _userRepository.AddAsync(user);
            //unit of work pattern
            await _unitOfWork.SaveChangesAsync();

            return new AuthResult { Success = true, User = user };
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _userRepository.IsEmailTakenAsync(email);
        }

        public async Task<bool> IsUsernameExists(string username)
        {
            return await _userRepository.IsUsernameTakenAsync(username);
        }

        //Hashing
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

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

