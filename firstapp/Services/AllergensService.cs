using firstapp.Models;
using firstapp.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Services
{
    public class AllergensService : IAllergensService
    {
        private readonly RecipeDbContext _recipeDbContext;
        public AllergensService(RecipeDbContext context)
        {
            _recipeDbContext = context;
        }

        private IQueryable<Allergens> GetBasedOnContainDeleted(bool containDeleted)
        {
            return containDeleted ? _recipeDbContext.Set<Allergens>().IgnoreQueryFilters() : _recipeDbContext.Set<Allergens>();
        }

        /// <summary>
        /// Összes allergén lekérdezése
        /// </summary>
        public IQueryable<Allergens> GetAllergens(bool containDeleted)
        {
            return GetBasedOnContainDeleted(containDeleted);
        }
    }
}