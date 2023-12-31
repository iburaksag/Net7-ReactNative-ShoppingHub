using AutoMapper;
using MediatR;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Application.Baskets.Commands.CreateBasket
{
	public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, int>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBasketCommandHandler(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == 0)
            {
                throw new ApplicationException($"User with Id {request.UserId} not found.");
            }

            var basket = new Basket
            {
                UserId = request.UserId,
                Status = Domain.Enums.Status.InProcess
            };

            await _basketRepository.AddAsync(basket);
            await _unitOfWork.SaveChangesAsync();

            return basket.Id;
        }
    }
}

