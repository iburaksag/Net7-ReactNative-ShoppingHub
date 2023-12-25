using ShoppingHub.Domain.Entities;
using ShoppingHub.Domain.Repositories.Common;

namespace ShoppingHub.Domain.Repositories
{
	public interface IUserRepository : IBaseRepository<User>
	{
        Task<User> GetByUsernameAsync(string username);
        Task<bool> IsEmailTakenAsync(string email);
        Task<bool> IsUsernameTakenAsync(string username);
    }
}

