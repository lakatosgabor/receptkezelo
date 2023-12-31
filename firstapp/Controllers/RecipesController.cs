using firstapp.Models.Entity;
using firstapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace firstapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesService _recipesService;
        private readonly IBasicMaterialService _basicMaterialService;
        private readonly IBasicMaterialCategoryService _basicMaterialCategoryService;
        private readonly IAllergensService _allergensService;

        public RecipesController(IRecipesService recipesService,
                        IBasicMaterialCategoryService basicMaterialCategoryService,
                        IBasicMaterialService basicMaterialService,
                        IAllergensService allergensService)
        {
            _recipesService = recipesService;
            _basicMaterialService = basicMaterialService;
            _basicMaterialCategoryService = basicMaterialCategoryService;
            _allergensService = allergensService;
        }

        [HttpGet]
        [Route("GetRecipes")]
        public IQueryable<Recipes> GetRecipes(bool containDeleted)
        {
            return _recipesService.GetRecipes(containDeleted);
        }

        [HttpGet]
        [Route("GetBasicMaterials")]
        public IQueryable<BasicMaterials> GetBasicMaterials(bool containDeleted)
        {
            return _basicMaterialService.GetBasicMaterials(containDeleted);
        }

        [HttpGet]
        [Route("GetBasicMaterialCategories")]
        public IQueryable<BasicMaterialCategories> GetBasicMaterialCategories(bool containDeleted)
        {
            return _basicMaterialCategoryService.GetBasicMaterialCategories(containDeleted);
        }


        [HttpGet]
        [Route("GetAllergens")]
        public IQueryable<Allergens> GetAllergens(bool containDeleted)
        {
            return _allergensService.GetAllergens(containDeleted);
        }

        [HttpGet]
        [Route("GetFullRecipe")]
        public IQueryable<object> GetFullRecipe(bool containDeleted, int recipeId)
        {
            return _recipesService.GetFullRecipe(containDeleted, recipeId);
        }


        [HttpGet]
        [Route("GetBasicMaterialsWithCategory")]
        public IQueryable<object> GetBasicMaterialsWithCategory(bool containDeleted, int categoryId)
        {
            return _basicMaterialService.GetBasicMaterialsWithCategory(containDeleted, categoryId);
        }

        [HttpGet]
        [Route("GetBasicMaterialsWithAllergen")]
        public IQueryable<object> GetBasicMaterialsWithAllergen(bool containDeleted, int allergenId)
        {
            return _basicMaterialService.GetBasicMaterialsWithAllergen(containDeleted, allergenId);
        }

        [HttpPost]
        [Route("SaveRecipe")]
        public IActionResult SaveRecipe(Recipes recipes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("A megadott adatok nem megfelelőek");
            }

            if (recipes.CookingTime == 0)
            {
                return BadRequest("A főzési idő megadása kötelező!");
            }

            return _recipesService.SaveRecipe(recipes);
        }

    }
}
