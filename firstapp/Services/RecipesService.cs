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
    }
}
