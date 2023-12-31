using System;
using AutoMapper;
using MediatR;
using ShoppingHub.Application.DTO;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _productRepository.GetByIdAsync(request.Id);

            if (productToDelete == null)
            {
                throw new InvalidOperationException($"Product with Id {request.Id} not found.");
            }


            await _productRepository.DeleteAsync(productToDelete);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}

