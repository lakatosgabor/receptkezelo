using firstapp.Models;
using firstapp.Models.Entity;
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

        /// <summary>
        /// Egy kategóriába tartozó összes alapanyag
        /// </summary>
        public IQueryable<Ingredients> GetIngredientsByCategory(bool containDeleted, int categoryId)
        {
            IQueryable<Ingredients> ingredientsQuery = GetBasedOnContainDeleted(containDeleted);

            return ingredientsQuery;
        }

        /// <summary>
        ///  Egy allergénhez tartozó összes alapanyag
        /// </summary>
        public IQueryable<Ingredients> GetIngredientsByAllergen(bool containDeleted, int allergenId)
        {
            IQueryable<Ingredients> ingredientsQuery = GetBasedOnContainDeleted(containDeleted);

            return ingredientsQuery;
        }
    }
}
