using Microsoft.EntityFrameworkCore;
using firstapp.Models.Entity;

namespace firstapp.Services
{
    public class IngredientGroupService : IIngredientGroupService
    {
        private readonly RecipeDbContext _recipeDbContext;

        public IngredientGroupService(RecipeDbContext context)
        {
            _recipeDbContext = context;
        }

        private IQueryable<IngredientGroups> GetBasedOnContainDeleted(bool containDeleted)
        {
            return containDeleted ? _recipeDbContext.Set<IngredientGroups>().IgnoreQueryFilters() : _recipeDbContext.Set<IngredientGroups>();
        }

        /// <summary>
        /// Hozzávaló csoport alap adatainak mentése
        /// </summary>
        public async Task<string> SaveIngredientGroup(IngredientGroups ingredientGroups)
        {
            try
            {
                _recipeDbContext.IngredientGroups.Add(ingredientGroups);
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
        /// Csoport hozzávaló entitás törlése
        /// </summary>
        public async Task<string> DeleteIngredientGroup(int ingredientGroupId)
        {
            try
            {
                var ingredientGroup = _recipeDbContext.IngredientGroups.FirstOrDefault(e => e.Id == ingredientGroupId);

                if (ingredientGroup == null)
                {
                    throw new InvalidOperationException("A hozzávaló csoport nem található az adatbázisban.");
                }

                var isUsedInOtherEntity = _recipeDbContext.IngredientGroupping.Any(e => e.IngredientGroupId == ingredientGroupId);

                if (isUsedInOtherEntity)
                {
                    throw new InvalidOperationException("A recept más entitásokhoz kapcsolódik, nem törölhető.");
                }

                ingredientGroup.IsDeleted = true;
                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres törlés!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }
        }

        /// <summary>
        /// Hozzávaló csoport entitás szerkesztése
        /// </summary>
        public async Task<string> UpdateIngredientGroup(IngredientGroups ingredientGroups)
        {
            try
            {
                var existingIngredientGroup = await _recipeDbContext.IngredientGroups.FirstOrDefaultAsync(r => r.Id == ingredientGroups.Id);
                if (existingIngredientGroup == null)
                {
                    throw new InvalidOperationException("A hozzávaló csoport nem található az adatbázisban.");
                }

                existingIngredientGroup.GroupName= ingredientGroups.GroupName;
                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres módosítás!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }

        }

        /// <summary>
        /// Összes hozzávaló csoport lekérdezése
        /// </summary>
        public IQueryable<IngredientGroups> GetIngredientGroups(bool containDeleted)
        {
            return GetBasedOnContainDeleted(containDeleted);
        }
    }
}
