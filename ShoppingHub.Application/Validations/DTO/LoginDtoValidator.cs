using System;
using FluentValidation;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.Validations.DTO
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(dto => dto.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(15).WithMessage("Username cannot exceed 15 characters.");

            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.");
        }
    }
}

