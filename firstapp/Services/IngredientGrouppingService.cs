using firstapp.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace firstapp.Services
{
    public class IngredientGrouppingService : IIngredientGrouppingService
    {
        private readonly RecipeDbContext _recipeDbContext;

        public IngredientGrouppingService(RecipeDbContext context)
        {
            _recipeDbContext = context;
        }


        /// <summary>
        /// Hozzávalók csoportosítása
        /// </summary>
        public async Task<string> SaveIngredientGroupping(IngredientGroupping ingredientGroupping)
        {
            try
            {
                _recipeDbContext.IngredientGroupping.Add(ingredientGroupping);
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
        /// Hozzávaló csoportból törlése
        /// </summary>
        public async Task<string> DeleteIngredientGroupping(int ingredientGrouppingId)
        {
            try
            {
                var connection = _recipeDbContext.IngredientGroupping.FirstOrDefault(e => e.Id == ingredientGrouppingId);

                if (connection == null)
                {
                    throw new InvalidOperationException("A kapcsolat nem található az adatbázisban.");
                }

                _recipeDbContext.IngredientGroupping.Remove(connection);
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
