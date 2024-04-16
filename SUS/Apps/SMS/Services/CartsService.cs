using SMS.Data;
using SMS.ViewModels.Carts;

namespace SMS.Services
{
	public class CartsService : ICartsService
	{
		private readonly ApplicationDbContext db;

		public CartsService(ApplicationDbContext db)
		{
			this.db = db;
		}

		public void AddProductToCart(string cartId, string productId)
		{
			var cart = this.db.Carts.FirstOrDefault(x => x.Id == cartId);

			if (cart != null)
			{
				var product = this.db.Products.FirstOrDefault(x => x.Id == productId);

				if (product != null)
				{
					cart.Products.Add(product);

					this.db.SaveChanges();
				}
			}
		}

		public bool DoesProductExist(string productId)
		{
			return this.db.Products.Any(x => x.Id == productId);
		}

		public ICollection<CartProductViewModel> GetUserCart(string userId)
		{
			var user = this.db.Users.FirstOrDefault(x => x.Id == userId);

			if (user != null)
			{
				return this.db.Carts.Where(x => x.UserId == userId)
					.Select(x => x.Products.Select(x => new CartProductViewModel
					{
						Id = x.Id,
						Name = x.Name,
						Price = x.Price
					}).ToList()
					).FirstOrDefault();
			}

			return null;
		}

		public string GetUserCartId(string userId)
		{
			var user = this.db.Users.FirstOrDefault(x => x.Id == userId);

			if (user != null)
			{
				return user.CartId;
			}

			return null;
		}

		public void BuyProductsInCart(string userId)
		{
			var cart = this.db.Carts.FirstOrDefault(x => x.UserId == userId);

			if (cart != null)
			{
				var products = this.db.Products.Where(x => x.CartId == cart.Id);

				if (products != null)
				{
					foreach (var product in products)
					{
						product.CartId = null;
					}

					this.db.SaveChanges();
				}
			}
		}

		public void RemoveProductFromCart(string userId, string productId)
		{
			var cart = this.db.Carts.FirstOrDefault(x => x.UserId == userId);

			if (cart != null)
			{
				var products = this.db.Products.Where(x => x.CartId == cart.Id);

				foreach (var product in products)
				{
					if (product.Id == productId)
					{
						product.CartId = null;
					}
				}

				this.db.SaveChanges();
			}
		}
	}
}
