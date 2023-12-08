using Microsoft.AspNetCore.Identity;

namespace Recipes.Models
{
    public class User : IdentityUser
    {

        public List<Recipe> Recipes { get; set; }
    }
}
