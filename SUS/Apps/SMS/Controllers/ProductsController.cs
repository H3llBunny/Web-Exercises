
using SMS.Services;
using SMS.ViewModels;
using SMS.ViewModels.Products;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SMS.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductsService productsService;

		public ProductsController(IProductsService productsService)
        {
			this.productsService = productsService;
		}

        public HttpResponse Create()
		{
			if (this.IsUserSignedIn())
			{
				return this.View();
			}

			return this.Redirect("/Users/Login");
		}

		[HttpPost]
		public HttpResponse Create(CreateProductInputModel input)
		{
			if (!this.IsUserSignedIn())
			{
				return this.Redirect("/Users/Login");

			}

			if (this.productsService.DoesProductAlreadyExist(input.Name))
			{
				var viewModel = new ErrorViewModel
				{
					Errors = new List<string> { "Product already exist." }
				};

				return this.ErrorMessage(viewModel, "/Error");
			}

			if (string.IsNullOrEmpty(input.Name) || input.Name.Length < 4 || input.Name.Length > 20)
			{
				var viewModel = new ErrorViewModel
				{
					Errors = new List<string> { "Product name should be between 4 and 20 characters." }
				};

				return this.ErrorMessage(viewModel, "/Error");
			}

			if (input.Price == null || input.Price < 0.05M || input.Price > 1000)
			{
				var viewModel = new ErrorViewModel
				{
					Errors = new List<string> { "Price should be in the range of 0.05 and 1000." }
				};

				return this.ErrorMessage(viewModel, "/Error");
			}

			this.productsService.CreateProduct(input.Name, input.Price);

			return this.Redirect("/Home/IndexLoggedIn");
		}
	}
}
