
using BusStation.Services;
using BusStation.ViewModels.Destinations;
using SUS.HTTP;
using SUS.MvcFramework;

namespace BusStation.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly IDestinationsService destinationsService;

        public DestinationsController(IDestinationsService destinationsService)
        {
            this.destinationsService = destinationsService;
        }

        public HttpResponse All()
        {

            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var destinationsViewModel = this.destinationsService.GetAllDestionations();

            return this.View(destinationsViewModel);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(DestinationInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(input.DestinationName) || input.DestinationName.Length < 2 || input.DestinationName.Length > 50)
            {
                return this.Error("Destination Name should be between 2 and 50 characters long.");
            }

            if (string.IsNullOrEmpty(input.Origin) || input.Origin.Length < 2 || input.Origin.Length > 50)
            {
                return this.Error("Origin Name should be between 2 and 50 characters long.");
            }

            if (input.Date == null)
            {
                return this.Error("Invalid date.");
            }

            if (input.Date < DateTime.UtcNow.Date)
            {
                return this.Error("Invalid date. Date cannot be in the past.");
            }

            if (input.Date.TimeOfDay < TimeSpan.Zero || input.Date.TimeOfDay >= TimeSpan.FromDays(1))
            {
                return this.Error("Invalid time.");
            }

            if (string.IsNullOrEmpty(input.ImageUrl))
            {
                return this.Error("Invalid image URL.");
            }

            DateTime dateTime = input.Date;

            this.destinationsService.AddNewDestinations(input.DestinationName, input.Origin, input.Date, dateTime.TimeOfDay, input.ImageUrl);

            return this.Redirect("/Destinations/All");
        }
    }
}
