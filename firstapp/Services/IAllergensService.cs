using firstapp.Models.Entity;

namespace firstapp.Services
{
    public interface IAllergensService
    {
        public IQueryable<Allergens> GetAllergens(bool containDeleted);
        public Task<string> SaveAllergen(Allergens Allergens);
        public Task<string> DeleteAllergen(int allergenId);
        public Task<string> UpdateAllergen(Allergens Allergens);

        public Task<string> SaveIngredientsAllergen(IngredientsAllergens IngredientsAllergens);
        public Task<string> DeleteIngredientsAllergen(int ingredientsAllergenId);

        public Task<string> AddUserAllergen(UserAllergens UserAllergens);

    }
}