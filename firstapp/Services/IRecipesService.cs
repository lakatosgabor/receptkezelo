using firstapp.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace firstapp.Services
{
    public interface IRecipesService
    {
        public IQueryable<Recipes> GetRecipes(bool containDeleted);
        public IQueryable<object> GetFullRecipe(bool containDeleted, int recipeId);
        public IActionResult SaveRecipe(Recipes recipes);
    }
}