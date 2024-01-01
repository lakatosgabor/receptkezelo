using firstapp.Models;
using firstapp.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Services
{
    public class AllergensService : IAllergensService
    {
        private readonly RecipeDbContext _recipeDbContext;
        public AllergensService(RecipeDbContext context)
        {
            _recipeDbContext = context;
        }

        private IQueryable<Allergens> GetBasedOnContainDeleted(bool containDeleted)
        {
            return containDeleted ? _recipeDbContext.Set<Allergens>().IgnoreQueryFilters() : _recipeDbContext.Set<Allergens>();
        }

        /// <summary>
        /// Összes allergén lekérdezése
        /// </summary>
        public IQueryable<Allergens> GetAllergens(bool containDeleted)
        {
            return GetBasedOnContainDeleted(containDeleted);
        }

        /// <summary>
        /// Allergén alap adatainak mentése
        /// </summary>
        public async Task<string> SaveAllergen(Allergens allergens)
        {
            try
            {
                _recipeDbContext.Allergens.Add(allergens);
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
        /// Allergén entitás törlése
        /// </summary>
        public async Task<string> DeleteAllergen(int allergenId)
        {
            try
            {
                var allergen = _recipeDbContext.Allergens.FirstOrDefault(e => e.Id == allergenId);

                if (allergen == null)
                {
                    throw new InvalidOperationException("Az allergén nem található az adatbázisban.");
                }

                var isUsedInOtherEntity = _recipeDbContext.IngredientsAllergens.Any(e => e.AllergenId == allergenId);

                if (isUsedInOtherEntity)
                {
                    throw new InvalidOperationException("Az allergén más entitásokhoz kapcsolódik, nem törölhető.");
                }

                allergen.IsDeleted = true;
                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres törlés!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }
        }

        /// <summary>
        /// Allergén entitás szerkesztése
        /// </summary>
        public async Task<string> UpdateAllergen(Allergens allergens)
        {
            try
            {
                var existingAllergen = await _recipeDbContext.Allergens.FirstOrDefaultAsync(r => r.Id == allergens.Id);
                if (existingAllergen == null)
                {
                    throw new InvalidOperationException("Az allergén nem található az adatbázisban.");
                }
                existingAllergen.Name = allergens.Name;


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