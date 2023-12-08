using Recipes.Models;

namespace Recipes.DataAccess.Repository.IRepository
{
    public interface IUserRecipeLikeRepository : IRepository<UserRecipeLike>
    {
        void Update(UserRecipeLike like);
    }
}
