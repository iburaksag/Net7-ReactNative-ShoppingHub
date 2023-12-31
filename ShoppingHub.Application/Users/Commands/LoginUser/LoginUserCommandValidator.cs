using FluentValidation;

namespace ShoppingHub.Application.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(dto => dto.loginDto.Email)
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.")
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(dto => dto.loginDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.");

        }
    }
}

