using firstapp.Models;

namespace firstapp.Services
{
    public interface IRecipesService
    {
        public IQueryable<Recipes> GetRecipes(bool containDeleted);

    }
}
