using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ShoppingHub.Application.DTO;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Application.Baskets.Commands.UpdateBasket
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, BasketDto>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateBasketCommandHandler(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BasketDto> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateBasketCommandValidator().ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var currentBasket = await _basketRepository.GetByIdAsync(request.BasketId);

            if (currentBasket == null)
                throw new ApplicationException($"Basket with Id {request.BasketId} not found.");

            currentBasket.IPAddress = _httpContextAccessor?.HttpContext.Connection.RemoteIpAddress?.ToString();
            currentBasket.OrderAddress = request.OrderAddress;
            currentBasket.OrderDate = DateTime.UtcNow;
            currentBasket.UpdatedAt = DateTime.UtcNow;
            currentBasket.OrderTotal = request.OrderTotal;
            currentBasket.Status = Domain.Enums.Status.Completed;

            await _basketRepository.UpdateAsync(currentBasket);
            await _unitOfWork.SaveChangesAsync();

            var basketDto = _mapper.Map<BasketDto>(currentBasket);
            return basketDto;
        }
    }

}

