using firstapp.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        /// <summary>
        /// Recept alap adatainak mentése
        /// </summary>
        public async Task<string> SaveRecipe(Recipes recipes)
        {
            try
            {
                if (recipes.CookingTime == null || recipes.CookingTime == 0)
                {
                    throw new ArgumentException("A főzési idő megadása kötelező.");
                }

                _recipeDbContext.Recipes.Add(recipes);
                int affectedRows = await _recipeDbContext.SaveChangesAsync();

                if (affectedRows <= 0)
                {
                    throw new InvalidOperationException("Sikertelen mentés!");
                }
                return "Mentés sikeres!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }
        }

        /// <summary>
        /// Recept entitás törlése
        /// </summary>
        public async Task<string> DeleteRecipe(int recipeId)
        {
            try
            {
                var recipe = _recipeDbContext.Recipes.FirstOrDefault(e => e.Id == recipeId);

                if (recipe == null)
                {
                    throw new InvalidOperationException("A recept nem található az adatbázisban.");
                }

                var isUsedInOtherEntity = _recipeDbContext.RecipeIngredients.Any(e => e.RecipeId == recipeId);

                if (isUsedInOtherEntity)
                {
                    throw new InvalidOperationException("A recept más entitásokhoz kapcsolódik, nem törölhető.");
                }

                recipe.IsDeleted= true;
                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres törlés!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }
        }

        /// <summary>
        /// Recept entitás szerkesztése
        /// </summary>
        public async Task<string> UpdateRecipe(Recipes recipes)
        {
            try
            {
                var existingRecipe = await _recipeDbContext.Recipes.FirstOrDefaultAsync(r => r.Id == recipes.Id);
                if (existingRecipe == null)
                {
                    throw new InvalidOperationException("A recept nem található az adatbázisban.");
                }

                if (recipes.CookingTime == null || recipes.CookingTime == 0)
                {
                    throw new ArgumentException("A főzési idő megadása kötelező.");
                }

                existingRecipe.Title = recipes.Title;
                existingRecipe.PrepareTime = recipes.PrepareTime;
                existingRecipe.CodeName = recipes.CodeName;
                existingRecipe.CookingTime = recipes.CookingTime;
                existingRecipe.Description = recipes.Description;

                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres módosítás!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }

        }
    }
}