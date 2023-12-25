using System;
using ShoppingHub.Domain.Repositories.Common;
using ShoppingHub.Infrastructure.Data;

namespace ShoppingHub.Infrastructure.Repositories.Common
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();   
        }
    }
}

