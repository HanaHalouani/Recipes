using System.ComponentModel.DataAnnotations;

namespace Recipes.Models
{
    public class UserRecipeLike
    {
        [Key]
        public int UserRecipeLikeId { get; set; }
        public int? RecipeId { get; set; }
        public string? UserId { get; set; }
    }
}
