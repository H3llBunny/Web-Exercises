
using SMS.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SMS.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartsService cartsService;

        public CartsController(ICartsService cartsService)
        {
            this.cartsService = cartsService;
        }

        public HttpResponse Details()
        {
            if (!this.IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var userCartViewModel = this.cartsService.GetUserCart(this.GetUserId());

            return this.View(userCartViewModel);
        }

        public HttpResponse Remove(string productId)
		{
			if (!this.IsUserSignedIn())
			{
				return Redirect("/Users/Login");
			}

            this.cartsService.RemoveProductFromCart(this.GetUserId(), productId);

            return this.Redirect("/Carts/Details");

		}

        public HttpResponse Buy()
        {

			if (!this.IsUserSignedIn())
			{
				return Redirect("/Users/Login");
			}

            this.cartsService.BuyProductsInCart(this.GetUserId());

            return this.Redirect("/Home/IndexLoggedIn");
		}
    }
}
