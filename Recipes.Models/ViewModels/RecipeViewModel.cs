using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Recipes.Models.ViewModels
{
    public class RecipeViewModel
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
        [RequiredForCreate(ErrorMessage = "Please select an image for the recipe.")]

        [DisplayName("Image of the Recipe")]
        public IFormFile? File { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        public string? OwningUserId { get; set; }
        public bool IsBookmarked { get; set; }
        public bool IsLiked { get; set; }
        public int? TotalLikes { get; set; } = 1;

        [Required]
        public string Level { get; set; }

        [Required]
        public int? TotalCalories { get; set; }

        [Required]
        public int? PeopleCount { get; set; }
        [Required]

        [Display(Name = "Duration Hours")]
        public int DurationHours { get; set; }
        [Required]
        [Display(Name = "Duration Minutes")]
        public int DurationMinutes { get; set; }
        public string Duration
        {
            get
            {
                return DurationHours.ToString() + "h" + DurationMinutes.ToString() + "min";
            }
            set
            {

            }
        }

        public RecipeViewModel()
        {
            ImageUrl = string.Empty;
        }



        public static implicit operator RecipeViewModel(Recipe recipe)
        {
            return new RecipeViewModel
            {
                Id = recipe.Id,
                RecipeName = recipe.RecipeName,
                RecipeDescription = recipe.RecipeDescription,
                ImageUrl = recipe.ImageUrl,
                OwningUserId = recipe.OwningUserId,
                TotalLikes = recipe.TotalLikes,
                TotalCalories = recipe.TotalCalories,
                PeopleCount = recipe.PeopleCount,
                RecipeIngredients = recipe.RecipeIngredients,
                Level = recipe.Level,
                Duration = recipe.CookingDuration


            };
        }
        public static implicit operator Recipe(RecipeViewModel vm)
        {
            return new Recipe
            {
                Id = vm.Id,
                RecipeName = vm.RecipeName,
                RecipeDescription = vm.RecipeDescription,
                ImageUrl = vm.ImageUrl,
                OwningUserId = vm.OwningUserId,
                TotalLikes = vm.TotalLikes,
                TotalCalories = vm.TotalCalories,
                PeopleCount = vm.PeopleCount,
                RecipeIngredients = vm.RecipeIngredients,
                Level = vm.Level,
                CookingDuration = vm.Duration

            };
        }
    }

}
