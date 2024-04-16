
using CarShop.Services;
using CarShop.ViewModels.Issues;
using SUS.HTTP;
using SUS.MvcFramework;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssuesService issuesService;
        private readonly IUsersService usersService;

        public IssuesController(IIssuesService issuesService, IUsersService usersService)
        {
            this.issuesService = issuesService;
            this.usersService = usersService;
        }

        public HttpResponse CarIssues(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var carModel = this.issuesService.GetCarById(carId);
            return this.View(carModel);
        }

        [HttpGet]
        public HttpResponse Add(string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var carModel = this.issuesService.GetCarById(carId);
            return this.View(carModel);
        }

        [HttpPost]
        public HttpResponse Add(AddIssueInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.issuesService.AddNewIssue(input.CarId, input.Description);

            return this.CarIssues(input.CarId);
        }

        public HttpResponse Fix(string issueId, string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (this.usersService.IsUserMechanic(this.GetUserId()))
            {
                this.issuesService.FixIssue(issueId, carId);

                return this.CarIssues(carId);
            }

            return this.CarIssues(carId);
        }

        public HttpResponse Delete(string issueId, string carId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.issuesService.DeleteIssue(issueId, carId);

            return this.CarIssues(carId);
        }
    }
}
