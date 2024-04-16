using SMS.Data;
using SMS.ViewModels.Products;

namespace SMS.Services
{
	public class ProductsService : IProductsService
	{
		private readonly ApplicationDbContext db;

		public ProductsService(ApplicationDbContext db)
        {
			this.db = db;
		}

		public bool DoesProductAlreadyExist(string name)
		{
			return this.db.Products.Any(x => x.Name == name);
		}
		
		public void CreateProduct(string name, decimal price)
		{
			var product = new Product { Name = name, Price = price };

			this.db.Products.Add(product);
			this.db.SaveChanges();
		}

		public ICollection<ProducViewModel> GetAllProducts()
		{
			return this.db.Products
				.Select(x => new ProducViewModel
				{
					Id = x.Id,
					Name = x.Name,
					Price = x.Price,
					CartId = x.CartId
				}).ToList();
		}
	}
}
