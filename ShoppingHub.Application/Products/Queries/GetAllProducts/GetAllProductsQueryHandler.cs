using AutoMapper;
using MediatR;
using ShoppingHub.Application.DTO;
using ShoppingHub.Domain.Repositories;

namespace ShoppingHub.Application.Products.Queries.GetAllProducts
{
	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
	{
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            if (products == null)
                throw new InvalidOperationException("No products found.");

            var productsDto = _mapper.Map<List<ProductDto>>(products);

            return productsDto;
        }
    }
}

