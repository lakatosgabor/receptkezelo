using firstapp.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Services
{
    public class RecipesService : IRecipesService
    {

        private readonly RecipeDbContext _recipeDbContext;

        public RecipesService(RecipeDbContext context)
        {
            _recipeDbContext = context;
        }

        private IQueryable<Recipes> GetBasedOnContainDeleted(bool containDeleted)
        {
            return containDeleted ? _recipeDbContext.Set<Recipes>().IgnoreQueryFilters() : _recipeDbContext.Set<Recipes>();
        }

        /// <summary>
        /// Összes recept lekérdezése
        /// </summary>
        public IQueryable<Recipes> GetRecipes(bool containDeleted)
        {
            return GetBasedOnContainDeleted(containDeleted);
        }

        /// <summary>
        /// Egy adott repcet lekérdezése, hozzávalókkal, leírással, allergénekkel
        /// </summary>
        public IQueryable<object> GetFullRecipe(bool containDeleted, int recipeId)
        {
            IQueryable<Recipes> recipesQuery = GetBasedOnContainDeleted(containDeleted);
            var fullRecipeQuery = recipesQuery
            .Join(_recipeDbContext.RecipeIngredients,
                recipe => recipe.Id,
                recipeIngredient => recipeIngredient.RecipeId,
                (recipe, recipeIngredient) => new { Recipe = recipe, RecipeIngredient = recipeIngredient })
            .Join(_recipeDbContext.Ingredients,
                combined => combined.RecipeIngredient.IngredientId,
                ingredient => ingredient.Id,
                (combined, ingredient) => new { combined.Recipe, combined.RecipeIngredient, Ingredient = ingredient })
            .Join(_recipeDbContext.BasicMaterials,
                combined => combined.Ingredient.BasicMaterialId,
                basicMaterial => basicMaterial.Id,
                (combined, basicMaterial) => new { combined.Recipe, combined.RecipeIngredient, combined.Ingredient, BasicMaterial = basicMaterial })
            .Join(_recipeDbContext.IngredientsAllergens,
                combined => combined.BasicMaterial.Id,
                ingredientAllergen => ingredientAllergen.IngredientId,
                (combined, ingredientAllergen) => new { combined.Recipe, combined.RecipeIngredient, combined.Ingredient, combined.BasicMaterial, IngredientAllergen = ingredientAllergen })
            .Join(_recipeDbContext.Allergens,
                combined => combined.IngredientAllergen.AllergenId,
                allergen => allergen.Id,
                (combined, allergen) => new
                {
                    Recipe = combined.Recipe,
                    RecipeIngredient = combined.RecipeIngredient,
                    Ingredient = combined.Ingredient,
                    BasicMaterial = combined.BasicMaterial,
                    IngredientAllergen = combined.IngredientAllergen,
                    Allergen = allergen
                });

            return fullRecipeQuery.Select(item => new
            {
                Recipe = item.Recipe,
                RecipeIngredient = item.RecipeIngredient,
                Ingredient = item.Ingredient,
                BasicMaterial = item.BasicMaterial,
                IngredientAllergen = item.IngredientAllergen,
                Allergen = item.Allergen
            });
        }

        /// <summary>
        /// Recept alap adatainak mentése
        /// </summary>
        public IActionResult SaveRecipe(Recipes recipes)
        {
            try
            {
                _recipeDbContext.Recipes.Add(recipes);
                int affectedRows = _recipeDbContext.SaveChanges();

                if (affectedRows <= 0)
                {
                    return new BadRequestObjectResult("Mentés nem sikerült.");
                }
                return new OkObjectResult("Mentés sikeres!");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Hiba történt a mentés során: {ex.Message}");
            }
        }

        /// <summary>
        /// Recept elkészítési ideje PrepareTime + CookingTime
        /// </summary>
        public object fullReadyTime(bool containDeleted)
        {
            IQueryable<Recipes> recipesQuery = GetBasedOnContainDeleted(containDeleted);
            var readyTimeQuery = recipesQuery
                                .Select(recipe => new
                                {
                                    Id = recipe.Id,
                                    ReadyTime = recipe.PrepareTime + recipe.CookingTime
                                });

            return readyTimeQuery.ToList();
        }
    }
}