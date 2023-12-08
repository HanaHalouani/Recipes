using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Recipes.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Recipe Name")]
        public string RecipeName { get; set; }
        [Required]
        [DisplayName("Ingredients")]
        public string RecipeIngredients { get; set; }
        [Required]
        [DisplayName("Description")]
        public string RecipeDescription { get; set; }

        [DisplayName("Image Url")]
        public string? ImageUrl { get; set; } = string.Empty;
        public string? OwningUserId { get; set; }
        public int? TotalLikes { get; set; }

        [Required]
        public string Level { get; set; }

        [Required]
        [DisplayName("Total Calories")]
        public int? TotalCalories { get; set; }

        [Required]
        [DisplayName("People Count")]
        public int? PeopleCount { get; set; }
        [Required]
        [DisplayName("Cooking Duration")]
        public string CookingDuration { get; set; }

    }
}