using System;
using AutoMapper;
using FluentValidation;
using MediatR;
using ShoppingHub.Application.DTO;
using ShoppingHub.Application.Validations;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateProductCommandValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            
            var existingProduct = await _productRepository.GetByIdAsync(request.Id);

            if (existingProduct == null)
            {
                throw new InvalidOperationException($"Product with Id {request.Id} not found.");
            }

            // Update the existing product entity with the new values
            existingProduct = _mapper.Map(request, existingProduct);

            await _productRepository.UpdateAsync(existingProduct);
            await _unitOfWork.SaveChangesAsync();

            var updatedProductDto = _mapper.Map<ProductDto>(existingProduct);
            return updatedProductDto;
        }
    }
}

