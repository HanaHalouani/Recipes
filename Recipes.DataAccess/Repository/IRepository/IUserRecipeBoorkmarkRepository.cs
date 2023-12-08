using Recipes.Models;

namespace Recipes.DataAccess.Repository.IRepository
{
    public interface IUserRecipeBoorkmarkRepository : IRepository<UserRecipeBookmark>
    {
        void Update(UserRecipeBookmark bookmark);
    }
}
