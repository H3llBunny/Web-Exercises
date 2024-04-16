
using SMS.ViewModels.Products;

namespace SMS.Services
{
	public interface IProductsService
	{
		void CreateProduct(string name, decimal price);

		bool DoesProductAlreadyExist(string name);

		ICollection<ProducViewModel> GetAllProducts();
	}
}
