using firstapp.Models.Entity;

namespace firstapp.Services
{
    public interface IRecipeIngredientService
    {
        public Task<string> SaveRecipeIngredient(RecipeIngredients RecipeIngredients);
        public Task<string> DeleteRecipeIngredient(int recipeIngredientId);
    }
}
