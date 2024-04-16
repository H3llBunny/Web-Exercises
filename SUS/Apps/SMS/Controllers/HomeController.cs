using SMS.Services;
using SMS.ViewModels;
using SMS.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
	private readonly IProductsService productsService;
        private readonly IUsersService usersService;
        private readonly ICartsService cartsService;

        public HomeController(IProductsService productsService, IUsersService usersService, ICartsService cartsService)
        {
            this.productsService = productsService;
            this.usersService = usersService;
            this.cartsService = cartsService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.IndexLoggedIn();
            }

            return this.View();
        }

        public HttpResponse IndexLoggedIn()
        {
            if (!this.IsUserSignedIn())
            {
				return this.Redirect("/Users/Register");
			}

            var allProducts = this.productsService.GetAllProducts();

            var username = this.usersService.GetUsername(this.GetUserId());

            var homepageViewModel = new HomePageViewModel { Username = username, AllProducts = allProducts };

            return this.View(homepageViewModel);
		}

        [HttpGet("/Carts/AddProduct")]
        public HttpResponse AddProduct(string productId)
        
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Register");
            }

            if (!this.cartsService.DoesProductExist(productId))
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Product does not exist." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            var cartId = this.cartsService.GetUserCartId(this.GetUserId());


            this.cartsService.AddProductToCart(cartId, productId);

            return this.Redirect("/Carts/Details");
        }
    }
}
