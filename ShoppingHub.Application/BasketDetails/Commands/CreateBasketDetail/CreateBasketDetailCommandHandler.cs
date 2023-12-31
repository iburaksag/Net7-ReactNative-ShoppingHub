using AutoMapper;
using FluentValidation;
using MediatR;
using ShoppingHub.Application.DTO;
using ShoppingHub.Application.Products.Commands.CreateProduct;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Application.BasketDetails.Commands.CreateBasketDetail
{
    public class CreateBasketDetailCommandHandler : IRequestHandler<CreateBasketDetailCommand, BasketDetailDto>
    {
        private readonly IBasketDetailRepository _basketDetailRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBasketDetailCommandHandler(IBasketDetailRepository basketDetailRepository, IBasketRepository basketRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _basketDetailRepository = basketDetailRepository;
            _basketRepository = basketRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BasketDetailDto> Handle(CreateBasketDetailCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateBasketDetailCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var currentBasket = await _basketRepository.GetByIdAsync(request.BasketId);
            if (currentBasket == null)
            {
                throw new ApplicationException("Basket not found");
            }

            var basketDetail = new BasketDetail
            {
                BasketId = currentBasket.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            await _basketDetailRepository.AddAsync(basketDetail);
            await _unitOfWork.SaveChangesAsync();

            var basketDetailDto = _mapper.Map<BasketDetailDto>(basketDetail);
            return basketDetailDto;
        }
    }


}

