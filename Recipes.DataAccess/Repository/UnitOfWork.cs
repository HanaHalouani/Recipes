using Recipes.DataAccess.Repository.IRepository;

namespace Recipes.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRecipeRepository RecipeRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }
        public IUserRecipeBoorkmarkRepository UserRecipeBoorkmarkRepository { get; private set; }

        public IUserRecipeLikeRepository UserRecipeLikeRepository { get; private set; }
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            RecipeRepository = new RecipeRepository(_db);
            UserRepository = new UserRepository(_db);
            UserRecipeBoorkmarkRepository = new UserRecipeBookmarkRepository(db);
            UserRecipeLikeRepository = new UserRecipeLikeRepository(db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
