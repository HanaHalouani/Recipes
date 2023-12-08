using Recipes.DataAccess.Repository.IRepository;
using Recipes.Models;

namespace Recipes.DataAccess.Repository
{
    public class UserRecipeBookmarkRepository : Repository<UserRecipeBookmark>, IUserRecipeBoorkmarkRepository
    {
        private ApplicationDbContext _db;

        public UserRecipeBookmarkRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(UserRecipeBookmark bookmark)
        {
            _db.UserRecipeBookmark.Update(bookmark);
        }
    }
}
