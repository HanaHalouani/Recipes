using Recipes.Models;

namespace Recipes.DataAccess.Repository.IRepository
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        void Update(Recipe recipe);
    }
}
