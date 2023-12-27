using Microsoft.EntityFrameworkCore;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Infrastructure.Data;
using ShoppingHub.Infrastructure.Repositories.Common;

namespace ShoppingHub.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _dbContext.Products.Where(p => p.Category == category).ToListAsync();
        }
    }
}

