using AutoMapper;
using MediatR;
using ShoppingHub.Application.DTO;
using ShoppingHub.Domain.Repositories;

namespace ShoppingHub.Application.BasketDetails.Queries.GetCurrentBasketDetails
{
	public class GetCurrentBasketDetailsQueryHandler : IRequestHandler<GetCurrentBasketDetailsQuery, List<BasketDetailDto>>
	{
        private readonly IBasketDetailRepository _basketDetailRepository;
        private readonly IMapper _mapper;

        public GetCurrentBasketDetailsQueryHandler(IBasketDetailRepository basketDetailRepository, IMapper mapper)
        {
            _basketDetailRepository = basketDetailRepository;
            _mapper = mapper;
        }

        public async Task<List<BasketDetailDto>> Handle(GetCurrentBasketDetailsQuery request, CancellationToken cancellationToken)
        {
            var currentBasketDetails = await _basketDetailRepository.GetByBasketIdAsync(request.BasketId);
                
            if (currentBasketDetails == null)
                throw new InvalidOperationException("No basket details found.");

            var basketDetailsDto = _mapper.Map<List<BasketDetailDto>>(currentBasketDetails);
            return basketDetailsDto;
        }
    }
}

