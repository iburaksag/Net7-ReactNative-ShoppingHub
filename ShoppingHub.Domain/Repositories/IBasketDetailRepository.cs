using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Domain.Repositories
{
	public interface IBasketDetailRepository : IBaseRepository<BasketDetail>
	{
        Task<IEnumerable<BasketDetail>> GetByBasketIdAsync(int basketId);
        Task<IEnumerable<BasketDetail>> GetByProductIdAsync(int productId);
    }
}

