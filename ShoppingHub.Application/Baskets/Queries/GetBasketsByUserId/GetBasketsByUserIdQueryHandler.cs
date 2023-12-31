using AutoMapper;
using MediatR;
using ShoppingHub.Application.DTO;
using ShoppingHub.Domain.Repositories;

namespace ShoppingHub.Application.Baskets.Queries.GetBasketsByUserId
{
    public class GetBasketsByUserIdQueryHandler : IRequestHandler<GetBasketsByUserIdQuery, List<BasketDto>>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public GetBasketsByUserIdQueryHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<List<BasketDto>> Handle(GetBasketsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var baskets = await _basketRepository.GetBasketsByUserIdWithOrderAsync(request.UserId);

            if (baskets == null)
                throw new InvalidOperationException("No baskets found.");

            var basketsDto = _mapper.Map<List<BasketDto>>(baskets);
            return basketsDto;
        }
    }
}

