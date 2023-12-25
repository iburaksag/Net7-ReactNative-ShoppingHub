using Microsoft.EntityFrameworkCore;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Infrastructure.Data;
using ShoppingHub.Infrastructure.Repositories.Common;

namespace ShoppingHub.Infrastructure.Repositories
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        public BasketRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Basket> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Baskets.SingleOrDefaultAsync(b => b.UserId == userId);
        }
    }
}

