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

        /// <summary>
        /// Egy kategóriába tartozó összes alapanyag
        /// </summary>
        public IQueryable<Ingredients> GetIngredientsByCategory(bool containDeleted, int categoryId)
        {
            IQueryable<Ingredients> ingredientsQuery = GetBasedOnContainDeleted(containDeleted);

            return ingredientsQuery
                .Where(i => i.CategoryId == categoryId)
                .Include(i => i.Categories) // Kategória betöltése
                .Include(i => i.Allergens); // Allergének betöltése
        }

        /// <summary>
        ///  Egy allergénhez tartozó összes alapanyag
        /// </summary>
        public IQueryable<Ingredients> GetIngredientsByAllergen(bool containDeleted, int allergenId)
        {
            IQueryable<Ingredients> ingredientsQuery = GetBasedOnContainDeleted(containDeleted);

            return ingredientsQuery
                .Where(i => i.Allergens.Any(a => a.AllergenId == allergenId))
                .Include(i => i.Categories) // Kategória betöltése
                .Include(i => i.Allergens); // Allergének betöltése
        }
    }
}
