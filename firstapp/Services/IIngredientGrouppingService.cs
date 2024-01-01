using firstapp.Models.Entity;

namespace firstapp.Services
{
    public interface IIngredientGrouppingService
    {
        public Task<string> SaveIngredientGroupping(IngredientGroupping ingredientGroupping);
        public Task<string> DeleteIngredientGroupping(int ingredientGrouppingId);
    }
}
