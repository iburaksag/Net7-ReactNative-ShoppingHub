using System;
namespace ShoppingHub.Domain.Repositories.Common
{
	public interface IUnitOfWork
	{
		Task SaveChangesAsync();
	}
}

