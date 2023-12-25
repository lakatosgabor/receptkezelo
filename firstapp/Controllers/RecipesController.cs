using firstapp.Models;
using firstapp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace firstapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesService _recipesService;
        private readonly IIngredientsService _ingredientsService;
        private readonly ICategoriesService _categoriesService;
        private readonly IAllergensService _allergensService;


        public RecipesController(IRecipesService recipesService,
                                IIngredientsService ingredientsService,
                                ICategoriesService categoriesService,
                                IAllergensService allergensService) 
        {
            _recipesService = recipesService;
            _ingredientsService = ingredientsService;
            _categoriesService = categoriesService;
            _allergensService = allergensService;
        }

        [HttpGet]
        [Route("GetRecipes")]
        public IQueryable<Recipes> GetRecipes(bool containDeleted)
        {
            return _recipesService.GetRecipes(containDeleted);
        }

        [HttpGet]
        [Route("GetFullRecipe")]
        public IQueryable<Recipes> GetFullRecipe(bool containDeleted, int recipeId)
        {
            return _recipesService.GetFullRecipe(containDeleted, recipeId);
        }

        [HttpGet]
        [Route("GetIngredients")]
        public IQueryable<Ingredients> GetIngredients(bool containDeleted)
        {
            return _ingredientsService.GetIngredients(containDeleted);
        }

        [HttpGet]
        [Route("GetIngredientsByCategory")]
        public IQueryable<Ingredients> GetIngredientsByCategory(bool containDeleted, int categoryId)
        {
            return _ingredientsService.GetIngredientsByCategory(containDeleted, categoryId);
        }

        [HttpGet]
        [Route("GetIngredientsByAllergen")]
        public IQueryable<Ingredients> GetIngredientsByAllergen(bool containDeleted, int allergenId)
        {
            return _ingredientsService.GetIngredientsByAllergen(containDeleted, allergenId);
        }

        [HttpGet]
        [Route("GetCategories")]
        public IQueryable<Categories> GetCategories(bool containDeleted)
        {
            return _categoriesService.GetCategories(containDeleted);
        }

        [HttpGet]
        [Route("GetAllergens")]
        public IQueryable<Allergens> GetAllergens(bool containDeleted)
        {
            return _allergensService.GetAllergens(containDeleted);
        }
    }
}
