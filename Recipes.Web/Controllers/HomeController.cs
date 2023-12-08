using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipes.DataAccess.Repository.IRepository;
using Recipes.Models;
using Recipes.Models.ViewModels;
using Recipes.Web.Models;
using System.Diagnostics;

namespace Recipes.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unityOfWork;
        private IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment, IUnitOfWork unityOfWork, SignInManager<User> signInManager, UserManager<User> userManager, IUserRepository userRepository)
        {
            _unityOfWork = unityOfWork;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            var listOfRecipes = _unityOfWork.RecipeRepository.GetAll().ToList();
            return View(listOfRecipes);
        }

        public async Task<IActionResult> Details(int? recipeId)
        {

            var recipe = _unityOfWork.RecipeRepository.Get(i => i.Id == recipeId);
            RecipeViewModel recipeNon = recipe;
            var ownerName = _unityOfWork.UserRepository.Get(u => u.Id == recipe.OwningUserId).UserName;

            var user = await _userManager.GetUserAsync(User);
            if (user is not null)
            {
                recipeNon.IsBookmarked = _unityOfWork.UserRecipeBoorkmarkRepository.GetAll().Any(b => b.UserId == user.Id && b.RecipeId == recipeId);
                recipeNon.IsLiked = _unityOfWork.UserRecipeLikeRepository.GetAll().Any(b => b.UserId == user.Id && b.RecipeId == recipeId);

            }

            return View(recipeNon);
        }

        public async Task<IActionResult> IncrementLikesAsync(int? recipeId)
        {
            var user = await _userManager.GetUserAsync(User);

            bool isLiked = _unityOfWork.UserRecipeLikeRepository.GetAll().Any(b => b.UserId == user.Id && b.RecipeId == recipeId);
            var recipe = _unityOfWork.RecipeRepository.Get(i => i.Id == recipeId);

            if (recipe != null && !isLiked)
            {
                var userRecipeBoormarked = new UserRecipeLike
                {
                    RecipeId = recipeId,
                    UserId = user.Id,

                };
                recipe.TotalLikes++;
                _unityOfWork.RecipeRepository.Update(recipe);
                _unityOfWork.UserRecipeLikeRepository.Add(userRecipeBoormarked);
                _unityOfWork.Save();

                return RedirectToAction("Details", new { recipeId = recipeId });
            }
            if (isLiked)
            {
                var userRecipeBoormarked = _unityOfWork.UserRecipeLikeRepository.Get(b => b.UserId == user.Id && b.RecipeId == recipeId);
                recipe.TotalLikes--;
                _unityOfWork.RecipeRepository.Update(recipe);
                _unityOfWork.UserRecipeLikeRepository.Delete(userRecipeBoormarked);
                _unityOfWork.Save();
                return RedirectToAction("Details", new { recipeId = recipeId });
            }
            else
                return NotFound();
        }

        public async Task<IActionResult> BookmarkAsync(int? recipeId)
        {

            bool alreadyBookmarked;
            var user = await _userManager.GetUserAsync(User);
            RecipeViewModel recipe = _unityOfWork.RecipeRepository.Get(r => r.Id == recipeId);
            recipe.IsBookmarked = _unityOfWork.UserRecipeBoorkmarkRepository.GetAll().Any(b => b.UserId == user.Id && b.RecipeId == recipeId);

            if (recipe.IsBookmarked)
            {
                var bookmarkTobeDeleted = _unityOfWork.UserRecipeBoorkmarkRepository.Get(b => b.UserId == user.Id && b.RecipeId == recipeId);
                _unityOfWork.UserRecipeBoorkmarkRepository.Delete(bookmarkTobeDeleted);
                _unityOfWork.Save();


                return RedirectToAction("Details", new { recipeId });
            }

            var userRecipeBoormarked = new UserRecipeBookmark
            {
                RecipeId = recipeId,
                UserId = user.Id,

            };
            _unityOfWork.UserRecipeBoorkmarkRepository.Update(userRecipeBoormarked);
            _unityOfWork.Save();
            return RedirectToAction("Details", new { recipeId });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}