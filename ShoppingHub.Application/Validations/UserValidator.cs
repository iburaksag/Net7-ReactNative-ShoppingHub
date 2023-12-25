using System;
using FluentValidation;
using ShoppingHub.Domain.Entities;

namespace ShoppingHub.Application.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(15).WithMessage("Username cannot exceed 15 characters.");

            RuleFor(user => user.PasswordSalt.ToString())
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(20).WithMessage("Password cannot exceed 20 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");
        }
    }
}

