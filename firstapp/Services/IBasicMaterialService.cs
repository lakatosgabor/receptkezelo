using firstapp.Models.Entity;

namespace firstapp.Services
{
    public interface IBasicMaterialService
    {
        public IQueryable<BasicMaterials> GetBasicMaterials(bool containDeleted);
        public IQueryable<object> GetBasicMaterialsWithCategory(bool containDeleted, int categoryId);
        public IQueryable<object> GetBasicMaterialsWithAllergen(bool containDeleted, int allergenId);
        public Task<string> SaveBasicMaterial(BasicMaterials basicMaterials);
        public Task<string> DeleteBasicMaterial(int basicMaterialId);
        public Task<string> UpdateBasicMaterial(BasicMaterials basicMaterials);

        public IQueryable<object> GetBasicMaterialFromRecipesWithAllergen(int basicMaterialId);


    }
}
