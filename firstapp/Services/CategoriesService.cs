using firstapp.Models;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly RecipeDbContext _recipeDbContext;
        public CategoriesService(RecipeDbContext context)
        {
            _recipeDbContext = context;
        }

        private IQueryable<Categories> GetBasedOnContainDeleted(bool containDeleted)
        {
            return containDeleted ? _recipeDbContext.Set<Categories>().IgnoreQueryFilters() : _recipeDbContext.Set<Categories>();
        }

        /// <summary>
        /// Összes alapanyag kategória lekérdezése
        /// </summary>
        public IQueryable<Categories> GetCategories(bool containDeleted)
        {
            return GetBasedOnContainDeleted(containDeleted);
        }
    }
}
