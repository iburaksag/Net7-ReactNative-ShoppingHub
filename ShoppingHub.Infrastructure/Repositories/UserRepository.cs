using System;
using Microsoft.EntityFrameworkCore;
using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories;
using ShoppingHub.Infrastructure.Data;
using ShoppingHub.Infrastructure.Repositories.Common;

namespace ShoppingHub.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsUsernameTakenAsync(string userName)
        {
            return await _dbContext.Users.AnyAsync(u => u.UserName == userName);
        }
    }
}

