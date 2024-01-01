using firstapp.Models;
using firstapp.Models.Entity;

namespace firstapp.Services
{
    public interface IIngredientsService
    {
        public Task<string> SaveIngredient(Ingredients ingredients);
        public Task<string> DeleteIngredient(int ingredientId);
        public Task<string> UpdateIngredient(Ingredients ingredients);
        public IQueryable<Ingredients> GetIngredients(bool containDeleted);

    }
}