
using SUS.HTTP;
using SUS.MvcFramework;

namespace BusStation.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet("/")]
		public HttpResponse Index()
		{
			if (this.IsUserSignedIn())
			{
				return this.Redirect("/Destinations/All");
			}

			return this.View();
		}
	}
}
