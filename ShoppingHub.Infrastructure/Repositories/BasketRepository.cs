using Microsoft.EntityFrameworkCore;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Enums;
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

        public async Task<List<Basket>> GetBasketsByUserIdAsync(int userId)
        {
            return await _dbContext.Baskets.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<List<Basket>> GetBasketsByUserIdWithOrderAsync(int userId)
        {
            var baskets = await GetBasketsByUserIdAsync(userId);
            var sortedBaskets = baskets.Where(b => b.Status == Status.Completed).OrderByDescending(b => b.CreatedAt).ToList();
            return sortedBaskets;
        }
    }
}

