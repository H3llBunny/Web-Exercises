
using SMS.Services;
using SMS.ViewModels;
using SMS.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using SUS.MvcFramework.ViewEngine;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SMS.Controllers
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
                return Redirect("/Home/IndexLoggedIn");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserSignedIn())
            {
                return Redirect("/Home/IndexLoggedIn");
            }

            var userId = this.usersService.GetUserId(username, password);

            if (string.IsNullOrEmpty(userId))
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Invalid username or password." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }
            else
            {
                this.SignIn(userId);
                return this.Redirect("/Home/IndexLoggedIn");
            }
        }

        public HttpResponse Register()
        {

            if (this.IsUserSignedIn())
            {
                return Redirect("/Home/IndexLoggedIn");
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
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Email already taken." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            if (!this.usersService.IsUsernameAvailable(input.Username))
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Username already taken." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            if (string.IsNullOrEmpty(input.Username) || input.Username.Length < 5 || input.Username.Length > 20)
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Username should be between 5 and 20 characters." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            if (!Regex.IsMatch(input.Username, @"^[a-zA-Z0-9\.]+$"))
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Invalid username. Only alphanumeric characters are allowed." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            if (string.IsNullOrWhiteSpace(input.Email) || !new EmailAddressAttribute().IsValid(input.Email))
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Invalid email." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            if (string.IsNullOrEmpty(input.Password) || input.Password.Length < 6 || input.Password.Length > 20)
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Invalid password. The password should be between 6 and 20 characters." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            if (input.Password != input.ConfirmPassword)
            {
                var viewModel = new ErrorViewModel
                {
                    Errors = new List<string> { "Passwords should be the same." }
                };

                return this.ErrorMessage(viewModel, "/Error");
            }

            this.usersService.CreateUser(input.Username, input.Email, input.Password);

            return Redirect("/Users/Login");
        }

		public HttpResponse Logout()
		{
			if (!this.IsUserSignedIn())
			{
				var viewModel = new ErrorViewModel
				{
					Errors = new List<string> { "Only logged-in users can logout." }
				};

				return this.ErrorMessage(viewModel, "/Error");
			}

			this.SignOut();
			return this.Redirect("/");
		}
	}
}
