using AutoMapper;
using FluentValidation;
using MediatR;
using ShoppingHub.Application.DTO;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateProductCommandValidator().ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var newProduct = _mapper.Map<Product>(request);

            await _productRepository.AddAsync(newProduct);
            await _unitOfWork.SaveChangesAsync();

            var createdProductDto = _mapper.Map<ProductDto>(newProduct);
            return createdProductDto;
        }
    }
}

