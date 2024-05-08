namespace MoiteRecepti.Web.ViewModels.Recipes
{
    using System.ComponentModel.DataAnnotations;

    public class RecipeIngredentInputModel
    {
        [Required]
        [MinLength(3)]
        public string IngredientName { get; set; }

        [Required]
        [MinLength(1)]
        public string Quantity { get; set; }
    }
}
