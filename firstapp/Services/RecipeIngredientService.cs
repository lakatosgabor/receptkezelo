using firstapp.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Services
{
    public class RecipeIngredientService : IRecipeIngredientService
    {
        private readonly RecipeDbContext _recipeDbContext;

        public RecipeIngredientService(RecipeDbContext context)
        {
            _recipeDbContext = context;
        }

        /// <summary>
        /// Recept és hozzávaló kapcsolat beáálítása
        /// </summary>
        public async Task<string> SaveRecipeIngredient(RecipeIngredients recipeIngredients)
        {
            try
            {
                _recipeDbContext.RecipeIngredients.Add(recipeIngredients);
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
        /// Recept és hozzávaló kapcsolat törlése
        /// </summary>
        public async Task<string> DeleteRecipeIngredient(int recipeIngredientId)
        {
            try
            {
                var connection = _recipeDbContext.RecipeIngredients.FirstOrDefault(e => e.Id == recipeIngredientId);

                if (connection == null)
                {
                    throw new InvalidOperationException("A kapcsolat nem található az adatbázisban.");
                }

                _recipeDbContext.RecipeIngredients.Remove(connection);
                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres törlés!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }
        }
    }
}
