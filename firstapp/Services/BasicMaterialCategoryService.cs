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

        /// <summary>
        /// Alapanyag kategóriák alap adatainak mentése
        /// </summary>
        public async Task<string> SaveBasicMaterialCategory(BasicMaterialCategories basicMaterialCategories)
        {
            try
            {
                _recipeDbContext.BasicMaterialCategories.Add(basicMaterialCategories);
                int affectedRows = await _recipeDbContext.SaveChangesAsync();

                if (affectedRows <= 0)
                {
                    throw new InvalidOperationException("Sikertelen mentés!");
                }
                return "Mentés sikeres!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }
        }

        /// <summary>
        /// Alapanyag kategóriák entitás törlése
        /// </summary>
        public async Task<string> DeleteBasicMaterialCategory(int basicMaterialCategoryId)
        {
            try
            {
                var basicMaterialCategories = _recipeDbContext.BasicMaterialCategories.FirstOrDefault(e => e.Id == basicMaterialCategoryId);

                if (basicMaterialCategories == null)
                {
                    throw new InvalidOperationException("Az alapanyag kategória nem található az adatbázisban.");
                }

                var isUsedInOtherEntity = _recipeDbContext.BasicMaterials.Any(e => e.BasicMaterialCategoryId == basicMaterialCategoryId);

                if (isUsedInOtherEntity)
                {
                    throw new InvalidOperationException("Az alapanyag kategória más entitásokhoz kapcsolódik, nem törölhető.");
                }

                basicMaterialCategories.IsDeleted = true;
                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres törlés!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }
        }

        /// <summary>
        /// Alapanyag kategóriák entitás szerkesztése
        /// </summary>
        public async Task<string> UpdateBasicMaterialCategory(BasicMaterialCategories basicMaterialCategories)
        {
            try
            {
                var existingCategory = await _recipeDbContext.BasicMaterialCategories.FirstOrDefaultAsync(r => r.Id == basicMaterialCategories.Id);
                if (existingCategory == null)
                {
                    throw new InvalidOperationException("Az alapanyag kategória nem található az adatbázisban.");
                }

                existingCategory.Name= basicMaterialCategories.Name;

                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres módosítás!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }

        }
    }
}
