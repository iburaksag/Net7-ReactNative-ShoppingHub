using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Domain.Repositories
{
	public interface IBasketRepository : IBaseRepository<Basket>
	{
		Task<List<Basket>> GetBasketsByUserIdAsync(int userId);
        Task<List<Basket>> GetBasketsByUserIdWithOrderAsync(int userId);
    }
}

