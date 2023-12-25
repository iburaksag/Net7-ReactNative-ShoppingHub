using System;
using FluentValidation;
using ShoppingHub.Application.DTO;

namespace ShoppingHub.Application.Validations.DTO
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(dto => dto.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(15).WithMessage("Username cannot exceed 15 characters.");

            RuleFor(dto => dto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.");

            RuleFor(dto => dto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

        }
    }
}

