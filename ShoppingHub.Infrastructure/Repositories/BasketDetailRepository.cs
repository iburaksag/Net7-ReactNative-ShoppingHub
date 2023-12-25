using System;
using Microsoft.EntityFrameworkCore;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Infrastructure.Data;
using ShoppingHub.Infrastructure.Repositories.Common;

namespace ShoppingHub.Infrastructure.Repositories
{
    public class BasketDetailRepository : BaseRepository<BasketDetail>, IBasketDetailRepository
    {
        public BasketDetailRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<BasketDetail>> GetByBasketIdAsync(int basketId)
        {
            return await _dbContext.BasketDetails.Where(bd => bd.BasketId == basketId).ToListAsync();
        }

        public async Task<IEnumerable<BasketDetail>> GetByProductIdAsync(int productId)
        {
            return await _dbContext.BasketDetails.Where(bd => bd.ProductId == productId).ToListAsync();
        }
    }
}

