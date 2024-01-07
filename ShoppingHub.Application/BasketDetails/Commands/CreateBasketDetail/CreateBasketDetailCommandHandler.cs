using AutoMapper;
using FluentValidation;
using MediatR;
using ShoppingHub.Application.DTO;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Application.BasketDetails.Commands.CreateBasketDetail
{
    public class CreateBasketDetailCommandHandler : IRequestHandler<CreateBasketDetailCommand, BasketDetailDto>
    {
        private readonly IBasketDetailRepository _basketDetailRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateBasketDetailCommandHandler(IBasketDetailRepository basketDetailRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _basketDetailRepository = basketDetailRepository;
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

            var basketDetail = await _basketDetailRepository.GetByBasketAndProductAsync(request.BasketId, request.ProductId);

            if (basketDetail != null)
            {
                // If the BasketDetail already exists, update the quantity
                basketDetail.Quantity += request.Quantity;
                await _basketDetailRepository.UpdateAsync(basketDetail);
            }
            else
            {
                // If the BasketDetail does not exist, create a new one
                basketDetail = new BasketDetail
                {
                    BasketId = request.BasketId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };

                await _basketDetailRepository.AddAsync(basketDetail);
            }

            await _unitOfWork.SaveChangesAsync();

            var basketDetailDto = _mapper.Map<BasketDetailDto>(basketDetail);
            return basketDetailDto;
        }
    }


}

