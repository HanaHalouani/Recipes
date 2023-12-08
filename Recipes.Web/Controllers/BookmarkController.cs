using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipes.DataAccess.Repository.IRepository;
using Recipes.Models;

namespace Recipes.Web.Controllers
{
    public class BookmarkController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unityOfWork;

        public BookmarkController(IUnitOfWork unityOfWork, SignInManager<User> signInManager, UserManager<User> userManager, IUserRepository userRepository)
        {
            _unityOfWork = unityOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            List<Recipe> listRceipesBookmared = new List<Recipe>();
            if (user is not null)
            {
                List<UserRecipeBookmark> listRecipeBookmarks = _unityOfWork.UserRecipeBoorkmarkRepository.GetAll().Where(i => i.UserId == user.Id).ToList();

                foreach (var userRecipeBookmark in listRecipeBookmarks)
                {
                    var jjj = _unityOfWork.RecipeRepository.Get(i => i.Id == userRecipeBookmark.RecipeId);
                    listRceipesBookmared.Add(jjj);

                }
                return View(listRceipesBookmared);
            }

            return RedirectToPage("/Account/Login", new { area = "Identity" });


        }
    }
}
