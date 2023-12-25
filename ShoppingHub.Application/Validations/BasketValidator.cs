using System;
using FluentValidation;
using ShoppingHub.Domain.Entities;

namespace ShoppingHub.Application.Validations
{
    public class BasketValidator : AbstractValidator<Basket>
    {
        public BasketValidator()
        {
            RuleFor(basket => basket.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(basket => basket.IPAddress)
                .MaximumLength(50).WithMessage("IP address cannot exceed 50 characters.");

            RuleFor(basket => basket.OrderAddress)
                .MaximumLength(255).WithMessage("Order address cannot exceed 255 characters.");

            RuleFor(basket => basket.OrderDate)
                .NotEmpty().WithMessage("Order date is required.");

            RuleFor(basket => basket.Status)
                .NotEmpty().WithMessage("Status is required.");

            RuleFor(basket => basket.BasketDetails)
                .Must(details => details != null && details.Any())
                .WithMessage("Basket must have at least one detail.");
        }
    }
}

