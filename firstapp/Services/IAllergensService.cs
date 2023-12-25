using firstapp.Models;

namespace firstapp.Services
{
    public interface IAllergensService
    {
        public IQueryable<Allergens> GetAllergens(bool containDeleted);

    }
}
