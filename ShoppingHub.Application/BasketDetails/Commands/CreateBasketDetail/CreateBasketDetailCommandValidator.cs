using System;
using FluentValidation;


namespace ShoppingHub.Application.BasketDetails.Commands.CreateBasketDetail
{
    public class CreateBasketDetailCommandValidator : AbstractValidator<CreateBasketDetailCommand>
    {
        public CreateBasketDetailCommandValidator()
        {
            RuleFor(bd => bd.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

        }
    }
}

