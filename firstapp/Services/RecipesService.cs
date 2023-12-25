using firstapp.Models;
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
        public IQueryable<Recipes> GetFullRecipe(bool containDeleted, int recipeId)
        {
            IQueryable<Recipes> recipesQuery = GetBasedOnContainDeleted(containDeleted);

            return recipesQuery
                .Where(r => r.RecipeId == recipeId)
                .Include(r => r.IngredientGroups) // IngredientGroups betöltése
                    .ThenInclude(ig => ig.Ingredients) // Ingredients betöltése IngredientGroups-on belül
                        .ThenInclude(i => i.Categories) // Categories betöltése Ingredients-en belül
                .Include(r => r.IngredientGroups) // IngredientGroups betöltése (második Include, mivel több Include van)
                    .ThenInclude(ig => ig.Ingredients) // Ingredients betöltése IngredientGroups-on belül
                        .ThenInclude(i => i.Allergens); // Allergens betöltése Ingredients-en belül
        }
    }
}
