using firstapp.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Services
{
    public class BasicMaterialCategoryService : IBasicMaterialCategoryService
    {
        private readonly RecipeDbContext _recipeDbContext;

        public BasicMaterialCategoryService(RecipeDbContext context)
        {
            _recipeDbContext = context;
        }

        private IQueryable<BasicMaterialCategories> GetBasedOnContainDeleted(bool containDeleted)
        {
            return containDeleted ? _recipeDbContext.Set<BasicMaterialCategories>().IgnoreQueryFilters() : _recipeDbContext.Set<BasicMaterialCategories>();
        }

        /// <summary>
        /// Összes alapanyag kategória lekérdezése
        /// </summary>
        public IQueryable<BasicMaterialCategories> GetBasicMaterialCategories(bool containDeleted)
        {
            return GetBasedOnContainDeleted(containDeleted);
        }
    }
}
