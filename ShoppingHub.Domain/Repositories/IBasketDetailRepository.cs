using System.Linq;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Domain.Repositories
{
	public interface IBasketDetailRepository : IBaseRepository<BasketDetail>
	{
        Task<ICollection<BasketDetail>> GetByBasketIdAsync(int basketId);
        Task<ICollection<BasketDetail>> GetByProductIdAsync(int productId);
        Task<BasketDetail> GetByBasketAndProductAsync(int basketId, int productId);
    }
}

