namespace Recipes.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IRecipeRepository RecipeRepository { get; }
        IUserRepository UserRepository { get; }

        IUserRecipeBoorkmarkRepository UserRecipeBoorkmarkRepository { get; }
        IUserRecipeLikeRepository UserRecipeLikeRepository { get; }

        void Save();
    }
}
