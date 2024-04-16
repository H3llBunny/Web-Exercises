
using BusStation.Services;
using BusStation.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BusStation.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Destinations/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Destinations/All");
            }

            var userId = this.usersService.GetUserId(username, password);

            if (string.IsNullOrEmpty(userId))
            {
                return this.Error("Invalid username or password.");
            }
            else
            {
                this.SignIn(userId);
                return this.Redirect("/Destinations/All");
            }
        }

        public HttpResponse Register()
        {

            if (this.IsUserSignedIn())
            {
                return Redirect("/Destinations/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {

            if (this.IsUserSignedIn())
            {
                return Redirect("/Home/IndexLoggedIn");
            }

            if (!this.usersService.IsEmailAvailable(input.Email))
            {
                return this.Error("Email already taken.");
            }

            if (!this.usersService.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username already taken.");
            }

            if (string.IsNullOrEmpty(input.Username) || input.Username.Length < 5 || input.Username.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters.");
            }

            if (!Regex.IsMatch(input.Username, @"^[a-zA-Z0-9\.]+$"))
            {
                return this.Error("Invalid username. Only alphanumeric characters are allowed.");
            }

            if (string.IsNullOrWhiteSpace(input.Email) || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error("Invalid email.");
            }

            if (string.IsNullOrEmpty(input.Password) || input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Invalid password. The password should be between 5 and 20 characters.");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Passwords should be the same.");
            }

            this.usersService.CreateUser(input.Username, input.Email, input.Password);

            return Redirect("/Destinations/All");
        }

        public HttpResponse Logout()
        {

            if (!this.IsUserSignedIn())
            {

                return this.Error("Only logged-in users can logout.");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
