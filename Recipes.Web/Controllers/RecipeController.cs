using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipes.DataAccess.Repository.IRepository;
using Recipes.Models;
using Recipes.Models.ViewModels;

namespace Recipes.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unityOfWork;
        private IWebHostEnvironment _webHostEnvironment;

        public RecipeController(IWebHostEnvironment webHostEnvironment, IUnitOfWork unityOfWork, SignInManager<User> signInManager, UserManager<User> userManager, IUserRepository userRepository)
        {
            _unityOfWork = unityOfWork;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is not null)
            {
                List<Recipe> listRceipes = _unityOfWork.RecipeRepository.GetAll().Where(i => i.OwningUserId == user.Id).ToList();
                return View(listRceipes);
            }

            return RedirectToPage("/Account/Login", new { area = "Identity" });


        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RecipeViewModel recipeVM)
        {

            var user = await _userManager.GetUserAsync(User);
            if (user is not null)
            {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(recipeVM.File.FileName);
                    string recipeImageDirectory = Path.Combine(wwwRootPath, @"image\recipe");
                    string recipePath = Path.Combine(recipeImageDirectory, fileName);

                    using (var fileStream =
                           new FileStream(recipePath, FileMode.Create))
                    {
                        recipeVM.File.CopyTo(fileStream);
                    }

                    recipeVM.ImageUrl = @"\image\recipe\" + fileName;

                    recipeVM.OwningUserId = user.Id;
                    recipeVM.TotalLikes = 0;

                    Recipe recipeNon = recipeVM;

                    _unityOfWork.RecipeRepository.Add(recipeNon);
                    _unityOfWork.Save();
                    TempData["success"] = " Recipe Created Successfully";
                    return RedirectToAction("Index");
                }
                return View(recipeVM);

            }

            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }




        public IActionResult Edit(int? id)
        {
            if (id is null)
                return NotFound();
            var recipe = _unityOfWork.RecipeRepository.Get(x => x.Id == id);
            if (recipe == null)
                return NotFound();
            RecipeViewModel recipeVmn = recipe;

            return View(recipeVmn);
        }

        [HttpPost]
        public IActionResult Edit(RecipeViewModel recipeVmn)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (recipeVmn.File is not null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(recipeVmn.File.FileName);
                    string recipeImageDirectory = Path.Combine(wwwRootPath, @"image\recipe");

                    if (!string.IsNullOrEmpty(recipeVmn.ImageUrl))
                    {
                        string oldRecipeImage = Path.Combine(wwwRootPath, recipeVmn.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldRecipeImage))
                        {
                            System.IO.File.Delete(oldRecipeImage);
                        }
                    }
                    using (var fileStream =
                           new FileStream(Path.Combine(recipeImageDirectory, fileName), FileMode.Create))
                    {
                        recipeVmn.File.CopyTo(fileStream);
                    }

                    recipeVmn.ImageUrl = @"\image\recipe\" + fileName;
                }
                Recipe recipe = recipeVmn;
                _unityOfWork.RecipeRepository.Update(recipe);
                _unityOfWork.Save();
                TempData["success"] = " Recipe Updated Successfully";
                return RedirectToAction("Index");
            }

            return View(recipeVmn);
        }

        #region APiCALLs
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            Recipe? recipeToDelete = _unityOfWork.RecipeRepository.Get(x => x.Id == Id);
            if (recipeToDelete is null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unityOfWork.RecipeRepository.Delete(recipeToDelete);
            _unityOfWork.Save();

            return Json(new { success = true, message = "Recipe deleted" });
        }


        #endregion
    }
}
