using firstapp.Models.Entity;

namespace firstapp.Services
{
    public interface IRecipesService
    {
        public IQueryable<Recipes> GetRecipes(bool containDeleted);
        public IQueryable<object> GetFullRecipe(bool containDeleted, int recipeId);
    }
}