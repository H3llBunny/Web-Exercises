
using CarShop.Data;
using CarShop.Services;
using CarShop.ViewModels.Cars;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Text.RegularExpressions;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IUsersService usersService;

        public CarsController(ICarsService carsService, IUsersService usersService)
        {
            this.carsService = carsService;
            this.usersService = usersService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();

            if (this.usersService.IsUserMechanic(userId))
            {
                var allCars = this.carsService.GetAllCarsWithUnfixedIssues();
                return this.View(allCars);
            }
            else
            {
                var clientCars = this.carsService.GetClientCars(userId);
                return this.View(clientCars);
            }
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.usersService.IsUserMechanic(this.GetUserId()))
            {
                return this.View();
            }
            else
            {
                return this.Redirect("/Cars/All");
            }
        }

        [HttpPost]
        public HttpResponse Add(AddCarViewModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(input.Model) || input.Model.Length < 5 || input.Model.Length > 20)
            {
                return this.Error("Car model should be between 5 or 20 characters.");
            }

            if (input.Year == null || input.Year < 0)
            {
                return this.Error("Year can't be null or negative.");
            }

            if (string.IsNullOrEmpty(input.ImageUrl))
            {
                return this.Error("Picture is required.");
            }

            if (!Regex.IsMatch(input.PlateNumber, @"^[A-Z]{2}\s?[0-9]{4}\s?[A-Z]{2}$"))
            {
                return this.Error("Invalid plate number.");
            }

            this.carsService.AddCar(input, this.GetUserId());

            return this.Redirect("/Cars/All");
        }
    }
}
