using firstapp.Models.Entity;

namespace firstapp.Services
{
    public interface IBasicMaterialService
    {
        public IQueryable<BasicMaterials> GetBasicMaterials(bool containDeleted);
        public IQueryable<object> GetBasicMaterialsWithCategory(bool containDeleted, int categoryId);
        public IQueryable<object> GetBasicMaterialsWithAllergen(bool containDeleted, int allergenId);
        
    }
}
