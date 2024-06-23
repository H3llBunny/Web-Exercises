namespace MoiteRecepti.Web.Controllers
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MoiteRecepti.Common;
    using MoiteRecepti.Data.Models;
    using MoiteRecepti.Services.Data;
    using MoiteRecepti.Services.Messaging;
    using MoiteRecepti.Web.ViewModels.Recipes;

    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IEmailSender emailSender;

        public RecipesController(
            ICategoriesService categoriesService, 
            IRecipesService recipesService, 
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IEmailSender emailSender)
        {
            this.categoriesService = categoriesService;
            this.recipesService = recipesService;
            this.userManager = userManager;
            this.environment = environment;
            this.emailSender = emailSender;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var inputModel = this.recipesService.GetById<EditRecipeInputModel>(id);
            inputModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            await this.recipesService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateRecipeInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateRecipeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }
            //// var userId = this.User.FindFirst(ClaimTypes.nameIdentifier).Value; <- getting userId from the claim (the cookie)
            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.recipesService.CreateAsync(input, user.Id, (this.environment.WebRootPath + "/images"));
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }

            return this.Redirect("/");
        }

        public IActionResult All(int id = 1)
        {
            const int ItemsPerPage = 12;

            if (id <= 0 || id > (int)Math.Ceiling((double)this.recipesService.GetCount() / ItemsPerPage))
            {
                return this.NotFound();
            }

            var viewModel = new RecipesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                Recipes = this.recipesService.GetAll<RecipeInListViewModel>(id, ItemsPerPage),
                RecipesCount = this.recipesService.GetCount(),
            };
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var recipe = this.recipesService.GetById<SingleRecipeViewModel>(id);
            return this.View(recipe);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.recipesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        public async Task<IActionResult> SendToEmail(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userEmail = await this.userManager.GetEmailAsync(user);

            var recipe = this.recipesService.GetById<RecipeInListViewModel>(id);
            var html = new StringBuilder();
            html.AppendLine($"<h1>{recipe.Name}</h1>");
            html.AppendLine($"<h3>{recipe.CategoryName}</h3>");
            html.AppendLine($"<img src=\"{recipe.ImageUrl}\" />");
            await this.emailSender.SendEmailAsync("bogo.dishlyanov@gmail.com", "MoiteRecepti", userEmail, recipe.Name, html.ToString());

            return this.RedirectToAction(nameof(this.ById), new { id });
        }
    }
}
