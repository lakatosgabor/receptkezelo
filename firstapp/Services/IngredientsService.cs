using firstapp.Models;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Services
{
    public class IngredientsService : IIngredientsService
    {

        private readonly RecipeDbContext _recipeDbContext;
        public IngredientsService(RecipeDbContext context)
        {
            _recipeDbContext = context;
        }

        private IQueryable<Ingredients> GetBasedOnContainDeleted(bool containDeleted)
        {
            return containDeleted ? _recipeDbContext.Set<Ingredients>().IgnoreQueryFilters() : _recipeDbContext.Set<Ingredients>();
        }

        /// <summary>
        /// Összes alapanyag lekérdezése
        /// </summary>
        public IQueryable<Ingredients> GetIngredients(bool containDeleted)
        {
            return GetBasedOnContainDeleted(containDeleted);
        }
    }
}
