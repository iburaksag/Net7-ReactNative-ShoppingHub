using AutoMapper;
using MediatR;
using ShoppingHub.Application.DTO;
using ShoppingHub.Domain.Repositories;

namespace ShoppingHub.Application.BasketDetails.Queries.GetBasketDetailsByBasketId
{
	public class GetBasketDetailsByBasketIdQueryHandler : IRequestHandler<GetBasketDetailsByBasketIdQuery, List<BasketDetailDto>>
    {
        private readonly IBasketDetailRepository _basketDetailRepository;
        private readonly IMapper _mapper;

        public GetBasketDetailsByBasketIdQueryHandler(IBasketDetailRepository basketDetailRepository, IMapper mapper)
        {
            _basketDetailRepository = basketDetailRepository;
            _mapper = mapper;
        }

        public async Task<List<BasketDetailDto>> Handle(GetBasketDetailsByBasketIdQuery request, CancellationToken cancellationToken)
        {
            var basketDetails = await _basketDetailRepository.GetByBasketIdAsync(request.BasketId);

            if (basketDetails == null)
                throw new InvalidOperationException("No basket details found.");

            var basketDetailsDto = _mapper.Map<List<BasketDetailDto>>(basketDetails);
            return basketDetailsDto;
        }
    }
}

