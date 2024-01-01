using firstapp.Models;
using firstapp.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Services
{
    public class IngredientsService : IIngredientsService
    {

        private readonly RecipeDbContext _recipeDbContext;
        public IngredientsService(RecipeDbContext context)
        {
            _recipeDbContext = context;
        }

        private IQueryable<Ingredients> GetBasedOnContainDeleted(bool containDeleted)
        {
            return containDeleted ? _recipeDbContext.Set<Ingredients>().IgnoreQueryFilters() : _recipeDbContext.Set<Ingredients>();
        }

        /// <summary>
        /// Hozzávalók alap adatainak mentése
        /// </summary>
        public async Task<string> SaveIngredient(Ingredients ingredients)
        {
            try
            {
                _recipeDbContext.Ingredients.Add(ingredients);
                int affectedRows = await _recipeDbContext.SaveChangesAsync();

                if (affectedRows <= 0)
                {
                    throw new InvalidOperationException("Sikertelen mentés!");
                }
                return "Mentés sikeres!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.InnerException.Message}");
            }
        }

        /// <summary>
        /// Hozzávaló entitás törlése
        /// </summary>
        public async Task<string> DeleteIngredient(int ingredientId)
        {
            try
            {
                var ingredient = _recipeDbContext.Ingredients.FirstOrDefault(e => e.Id == ingredientId);

                if (ingredient == null)
                {
                    throw new InvalidOperationException("A hozzávaló nem található az adatbázisban.");
                }

                var isUsedInOtherEntity = _recipeDbContext.RecipeIngredients.Any(e => e.IngredientId == ingredientId);

                if (isUsedInOtherEntity)
                {
                    throw new InvalidOperationException("A hozzávaló más entitásokhoz kapcsolódik, nem törölhető.");
                }

                ingredient.IsDeleted = true;
                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres törlés!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }
        }

        /// <summary>
        /// Hozzávaló entitás szerkesztése
        /// </summary>
        public async Task<string> UpdateIngredient(Ingredients ingredients)
        {
            try
            {
                var existingIngredient = await _recipeDbContext.Ingredients.FirstOrDefaultAsync(r => r.Id == ingredients.Id);
                if (existingIngredient == null)
                {
                    throw new InvalidOperationException("A hozzávaló nem található az adatbázisban.");
                }

                existingIngredient.Unit = ingredients.Unit;
                existingIngredient.Quantity = ingredients.Quantity;
                existingIngredient.BasicMaterialId = ingredients.BasicMaterialId;
                existingIngredient.Name = ingredients.Name;

                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres módosítás!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.InnerException.Message}");
            }

        }

    }
}
