using Refactoring.Dto;

namespace Refactoring.Repositories
{
	public interface IProductRepository
	{
		Product? GetProduct(int productId, string productType);
	}
}
