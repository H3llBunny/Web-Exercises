
using SharedTrip.Services;
using SharedTrip.ViewModels;
using SharedTrip.ViewModels.Trips;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Globalization;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var tripsViewModel = this.tripsService.GetAll();

            return this.View(tripsViewModel);
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
        public HttpResponse Add(TripViewModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(model.StartPoint))
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "StartPoint cannot be null." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            if (string.IsNullOrEmpty(model.EndPoint))
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "EndPoint cannot be null." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            if (model.DepartureTime == null)
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "DepartureTime cannot be null." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            if (model.Seats == null || model.Seats < 2 || model.Seats > 6)
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Seats should be between 2 and 6." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            if (string.IsNullOrEmpty(model.Description) || model.Description.Length > 80)
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Description should not be more than 80 characters." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            if (string.IsNullOrEmpty(model.ImagePath))
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Description should not be null." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            this.tripsService.AddTrip(model);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
			if (!this.IsUserSignedIn())
			{
				return this.Redirect("/Users/Login");
			}

            var viewModel = this.tripsService.GetByTripId(tripId);

            return this.View(viewModel);
		}

        public HttpResponse AddUserToTrip(string tripId)
        {
			if (!this.IsUserSignedIn())
			{
				return this.Redirect("/Users/Login");
			}

            var userId = this.GetUserId();

            if (this.tripsService.IsUserAlreadyJoinedTrip(userId, tripId))
            {
				var viewModel = new ErrorViewModel
				{
					Errors = new List<string> { "User already in the trip." }
				};

				return this.ErrorMessage(viewModel, "/Error");
			}

            if (this.tripsService.AddUserToTrip(userId, tripId))
            {
                return this.Redirect("/Trips/All");
            }
            else
            {
				var viewModel = new ErrorViewModel
				{
					Errors = new List<string> { "No seats left." }
				};

				return this.ErrorMessage(viewModel, "/Error");
			}
		}
	}
}
