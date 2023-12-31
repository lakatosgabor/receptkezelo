using firstapp.Models;
using firstapp.Models.Entity;

namespace firstapp.Services
{
    public interface IIngredientsService
    {
        public IQueryable<Ingredients> GetIngredients(bool containDeleted);
        public IQueryable<Ingredients> GetIngredientsByCategory(bool containDeleted, int categoryId);
        public IQueryable<Ingredients> GetIngredientsByAllergen(bool containDeleted, int allergenId);

    }
}