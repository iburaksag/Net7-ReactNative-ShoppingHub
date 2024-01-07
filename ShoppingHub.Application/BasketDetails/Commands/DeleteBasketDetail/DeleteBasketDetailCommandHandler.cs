using MediatR;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Application.BasketDetails.Commands.DeleteBasketDetail
{
    public class DeleteBasketDetailCommandHandler : IRequestHandler<DeleteBasketDetailCommand, bool>
    {
        private readonly IBasketDetailRepository _basketDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBasketDetailCommandHandler(IBasketDetailRepository basketDetailRepository, IUnitOfWork unitOfWork)
        {
            _basketDetailRepository = basketDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBasketDetailCommand request, CancellationToken cancellationToken)
        {
            var basketDetail = await _basketDetailRepository.GetByIdAsync(request.Id);

            if (basketDetail == null)
                throw new ApplicationException("BasketDetail not found");

            await _basketDetailRepository.DeleteAsync(basketDetail);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }

}

