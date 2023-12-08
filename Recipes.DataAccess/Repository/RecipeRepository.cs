using Recipes.DataAccess.Repository.IRepository;
using Recipes.Models;

namespace Recipes.DataAccess.Repository
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        private ApplicationDbContext _db;

        public RecipeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Recipe recipe)
        {
            _db.Recipes.Update(recipe);
        }

    }
}
