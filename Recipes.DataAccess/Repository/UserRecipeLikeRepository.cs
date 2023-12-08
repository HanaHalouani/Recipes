using Recipes.DataAccess.Repository.IRepository;
using Recipes.Models;

namespace Recipes.DataAccess.Repository
{
    public class UserRecipeLikeRepository : Repository<UserRecipeLike>, IUserRecipeLikeRepository
    {
        private ApplicationDbContext _db;

        public UserRecipeLikeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UserRecipeLike like)
        {
            _db.UserRecipeLikes.Update(like);
        }
    }
}
