using firstapp.Models;

namespace firstapp.Services
{
    public interface IIngredientsService
    {
        public IQueryable<Ingredients> GetIngredients(bool containDeleted);

    }
}
