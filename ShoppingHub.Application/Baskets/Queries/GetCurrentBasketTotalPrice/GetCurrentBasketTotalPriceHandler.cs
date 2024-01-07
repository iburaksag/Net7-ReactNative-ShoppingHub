using System;
using AutoMapper;
using MediatR;
using ShoppingHub.Domain.Repositories;

namespace ShoppingHub.Application.Baskets.Queries.GetBasketTotalPrice
{
	public class GetCurrentBasketTotalPriceCommandHandler : IRequestHandler<GetCurrentBasketTotalPriceCommand, double>
	{
        private readonly IBasketDetailRepository _basketDetailRepository;
        private readonly IProductRepository _productRepository;

        public GetCurrentBasketTotalPriceCommandHandler(IBasketDetailRepository basketDetailRepository, IProductRepository productRepository)
        {
            _basketDetailRepository = basketDetailRepository;
            _productRepository = productRepository;
        }

        public async Task<double> Handle(GetCurrentBasketTotalPriceCommand request, CancellationToken cancellationToken)
        {
            double orderTotal = 0;
            var basketDetails = await _basketDetailRepository.GetByBasketIdAsync(request.BasketId);

            foreach (var basketDetail in basketDetails)
            {
                var product = await _productRepository.GetByIdAsync(basketDetail.ProductId);
                orderTotal += product.UnitPrice * basketDetail.Quantity;
            }

            return orderTotal;
        }
    }
}

