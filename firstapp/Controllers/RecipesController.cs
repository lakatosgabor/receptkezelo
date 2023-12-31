﻿using firstapp.Models.Entity;
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
        private readonly IIngredientsService _ingredientsService;
        private readonly IIngredientGroupService _ingredientGroupService;
        private readonly IRecipeIngredientService _recipeIngredientService;
        private readonly IIngredientGrouppingService _ingredientGrouppingService;

        public RecipesController(IRecipesService recipesService,
                        IBasicMaterialCategoryService basicMaterialCategoryService,
                        IBasicMaterialService basicMaterialService,
                        IAllergensService allergensService,
                        IIngredientsService ingredientsService,
                        IIngredientGroupService ingredientGroupService,
                        IRecipeIngredientService recipeIngredientService,
                        IIngredientGrouppingService ingredientGrouppingService)
        {
            _recipesService = recipesService;
            _basicMaterialService = basicMaterialService;
            _basicMaterialCategoryService = basicMaterialCategoryService;
            _allergensService = allergensService;
            _ingredientsService = ingredientsService;
            _ingredientGroupService = ingredientGroupService;
            _recipeIngredientService = recipeIngredientService;
            _ingredientGrouppingService = ingredientGrouppingService;
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
        public async Task<string> SaveRecipe(Recipes recipes)
        {
            return await _recipesService.SaveRecipe(recipes);
        }

        [HttpDelete]
        [Route("DeleteRecipe")]
        public async Task<string> DeleteRecipe(int recipeId)
        {
            if (recipeId <= 0)
            {
                return "A recept azonosító megadása kötelező!";
            }

            return await _recipesService.DeleteRecipe(recipeId);
        }


        [HttpPut]
        [Route("UpdateRecipe")]
        public async Task<string> UpdateRecipe(Recipes recipes)
        {
            return await _recipesService.UpdateRecipe(recipes);
        }

        [HttpPost]
        [Route("SaveIngredient")]
        public async Task<string> SaveIngredient(Ingredients ingredients)
        {
            return await _ingredientsService.SaveIngredient(ingredients);
        }

        [HttpGet]
        [Route("GetIngredients")]
        public IQueryable<Ingredients> GetIngredients(bool containDeleted)
        {
            return _ingredientsService.GetIngredients(containDeleted);
        }

        [HttpDelete]
        [Route("DeleteIngredient")]
        public async Task<string> DeleteIngredient(int ingredientId)
        {
            if (ingredientId <= 0)
            {
                return "A hozzávaló azonosító megadása kötelező!";
            }

            return await _ingredientsService.DeleteIngredient(ingredientId);
        }


        [HttpPut]
        [Route("UpdateIngredient")]
        public async Task<string> UpdateIngredient(Ingredients ingredients)
        {
            return await _ingredientsService.UpdateIngredient(ingredients);
        }

        [HttpPost]
        [Route("SaveIngredientGroup")]
        public async Task<string> SaveIngredientGroup(IngredientGroups ingredientGroups)
        {
            return await _ingredientGroupService.SaveIngredientGroup(ingredientGroups);
        }

        [HttpGet]
        [Route("GetIngredientGroups")]
        public IQueryable<IngredientGroups> GetIngredientGroups(bool containDeleted)
        {
            return _ingredientGroupService.GetIngredientGroups(containDeleted);
        }

        [HttpDelete]
        [Route("DeleteIngredientGroup")]
        public async Task<string> DeleteIngredientGroup(int ingredientGroupId)
        {
            if (ingredientGroupId <= 0)
            {
                return "A hozzávaló csoport azonosító megadása kötelező!";
            }

            return await _ingredientGroupService.DeleteIngredientGroup(ingredientGroupId);
        }


        [HttpPut]
        [Route("UpdateIngredientGroup")]
        public async Task<string> UpdateIngredientGroup(IngredientGroups ingredientGroups)
        {
            return await _ingredientGroupService.UpdateIngredientGroup(ingredientGroups);
        }

        [HttpPost]
        [Route("SaveBasicMaterial")]
        public async Task<string> SaveBasicMaterial(BasicMaterials basicMaterials)
        {
            return await _basicMaterialService.SaveBasicMaterial(basicMaterials);
        }

        [HttpDelete]
        [Route("DeleteBasicMaterial")]
        public async Task<string> DeleteBasicMaterial(int basicMaterialId)
        {
            if (basicMaterialId <= 0)
            {
                return "Az alapanyag azonosító megadása kötelező!";
            }

            return await _basicMaterialService.DeleteBasicMaterial(basicMaterialId);
        }


        [HttpPut]
        [Route("UpdateBasicMaterial")]
        public async Task<string> UpdateBasicMaterial(BasicMaterials basicMaterials)
        {
            return await _basicMaterialService.UpdateBasicMaterial(basicMaterials);
        }

        [HttpPost]
        [Route("SaveBasicMaterialCategory")]
        public async Task<string> SaveBasicMaterialCategory(BasicMaterialCategories basicMaterialCategories)
        {
            return await _basicMaterialCategoryService.SaveBasicMaterialCategory(basicMaterialCategories);
        }

        [HttpDelete]
        [Route("DeleteBasicMaterialCategory")]
        public async Task<string> DeleteBasicMaterialCategory(int basicMaterialCategoryId)
        {
            if (basicMaterialCategoryId <= 0)
            {
                return "Az alapanyag azonosító megadása kötelező!";
            }

            return await _basicMaterialCategoryService.DeleteBasicMaterialCategory(basicMaterialCategoryId);
        }


        [HttpPut]
        [Route("UpdateBasicMaterialCategory")]
        public async Task<string> UpdateBasicMaterialCategory(BasicMaterialCategories basicMaterialCategories)
        {
            return await _basicMaterialCategoryService.UpdateBasicMaterialCategory(basicMaterialCategories);
        }

        [HttpPost]
        [Route("SaveAllergen")]
        public async Task<string> SaveAllergen(Allergens allergens)
        {
            return await _allergensService.SaveAllergen(allergens);
        }

        [HttpDelete]
        [Route("DeleteAllergen")]
        public async Task<string> DeleteAllergen(int allergenId)
        {
            if (allergenId <= 0)
            {
                return "Az allergén azonosító megadása kötelező!";
            }

            return await _allergensService.DeleteAllergen(allergenId);
        }


        [HttpPut]
        [Route("UpdateAllergen")]
        public async Task<string> UpdateAllergen(Allergens allergens)
        {
            return await _allergensService.UpdateAllergen(allergens);
        }

        [HttpPost]
        [Route("SaveRecipeIngredient")]
        public async Task<string> SaveRecipeIngredient(RecipeIngredients RecipeIngredient)
        {
            return await _recipeIngredientService.SaveRecipeIngredient(RecipeIngredient);
        }

        [HttpDelete]
        [Route("DeleteRecipeIngredient")]
        public async Task<string> DeleteRecipeIngredient(int recipeIngredientId)
        {
            if (recipeIngredientId <= 0)
            {
                return "Az azonosító megadása kötelező!";
            }

            return await _recipeIngredientService.DeleteRecipeIngredient(recipeIngredientId);
        }

        [HttpPost]
        [Route("SaveIngredientGroupping")]
        public async Task<string> SaveIngredientGroupping(IngredientGroupping IngredientGroupping)
        {
            return await _ingredientGrouppingService.SaveIngredientGroupping(IngredientGroupping);
        }

        [HttpDelete]
        [Route("DeleteIngredientGroupping")]
        public async Task<string> DeleteIngredientGroupping(int ingredientGrouppingId)
        {
            if (ingredientGrouppingId <= 0)
            {
                return "Az azonosító megadása kötelező!";
            }

            return await _ingredientGrouppingService.DeleteIngredientGroupping(ingredientGrouppingId);
        }

        [HttpPost]
        [Route("SaveIngredientsAllergen")]
        public async Task<string> SaveIngredientsAllergen(IngredientsAllergens IngredientsAllergens)
        {
            return await _allergensService.SaveIngredientsAllergen(IngredientsAllergens);
        }

        [HttpDelete]
        [Route("DeleteIngredientsAllergen")]
        public async Task<string> DeleteIngredientsAllergen(int ingredientsAllergenId)
        {
            if (ingredientsAllergenId <= 0)
            {
                return "Az azonosító megadása kötelező!";
            }

            return await _allergensService.DeleteIngredientsAllergen(ingredientsAllergenId);
        }

        [HttpGet]
        [Route("GetBasicMaterialFromRecipesWithAllergen")]
        public IQueryable<object> GetBasicMaterialFromRecipesWithAllergen(int basicMaterialId)
        {
            return _basicMaterialService.GetBasicMaterialFromRecipesWithAllergen(basicMaterialId);
        }

        [HttpPost]
        [Route("AddFavoriteRecipe")]
        public async Task<string> AddFavoriteRecipe(FavoriteRecipes FavoriteRecipes)
        {
            return await _recipesService.AddFavoriteRecipe(FavoriteRecipes);
        }

        [HttpDelete]
        [Route("RemoveFavoriteRecipe")]
        public async Task<string> RemoveFavoriteRecipe(int favoriteRecipeId)
        {
            if (favoriteRecipeId <= 0)
            {
                return "Az azonosító megadása kötelező!";
            }

            return await _recipesService.RemoveFavoriteRecipe(favoriteRecipeId);
        }

        [HttpPost]
        [Route("AddUserAllergen")]
        public async Task<string> AddUserAllergen(UserAllergens UserAllergens)
        {
            return await _allergensService.AddUserAllergen(UserAllergens);
        }

        [HttpGet]
        [Route("GetUserAllergens")]
        public IQueryable<object> GetUserAllergens(bool containDeleted)
        {
            return _recipesService.GetUserAllergens(containDeleted);
        }
    }
}
