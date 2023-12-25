using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Domain.Repositories
{
	public interface IBasketRepository : IBaseRepository<Basket>
	{
		Task<Basket> GetByUserIdAsync(int userId);
	}
}

