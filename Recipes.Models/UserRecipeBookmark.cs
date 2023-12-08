using System.ComponentModel.DataAnnotations;

namespace Recipes.Models
{
    public class UserRecipeBookmark
    {
        [Key]
        public int UserRecipeBookmarkId { get; set; }
        public int? RecipeId { get; set; }
        public string? UserId { get; set; }
    }
}
