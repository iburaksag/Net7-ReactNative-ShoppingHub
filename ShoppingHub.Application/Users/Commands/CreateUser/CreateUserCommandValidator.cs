using FluentValidation;

namespace ShoppingHub.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(dto => dto.registerDto.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(15).WithMessage("Username cannot exceed 15 characters.");

            RuleFor(dto => dto.registerDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.");

            RuleFor(dto => dto.registerDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");
        }
    }

}

