using System;
using FluentValidation;
using ShoppingHub.Application.Products.Commands.UpdateProduct;

namespace ShoppingHub.Application.Baskets.Commands.UpdateBasket
{
	public class UpdateBasketCommandValidator : AbstractValidator<UpdateBasketCommand>
    {
		public UpdateBasketCommandValidator()
		{
            RuleFor(basket => basket.OrderAddress)
                .MaximumLength(255).WithMessage("Order address cannot exceed 500 characters.");

            RuleFor(basket => basket.OrderTotal)
                .NotEmpty().WithMessage("Order Total is required.")
                .GreaterThan(0).WithMessage("Order Total must be greater than 0.");
        }
	}
}

