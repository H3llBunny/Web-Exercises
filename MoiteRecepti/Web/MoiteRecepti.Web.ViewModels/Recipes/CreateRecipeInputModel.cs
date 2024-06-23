namespace MoiteRecepti.Web.ViewModels.Recipes
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateRecipeInputModel : BaseRecipeInputModel
    {
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<RecipeIngredentInputModel> Ingredients { get; set; }
    }
}
