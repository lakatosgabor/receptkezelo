using firstapp.Models.Entity;

namespace firstapp.Services
{
    public interface IBasicMaterialCategoryService
    {
        public IQueryable<BasicMaterialCategories> GetBasicMaterialCategories(bool containDeleted);
        public Task<string> SaveBasicMaterialCategory(BasicMaterialCategories basicMaterialCategories);
        public Task<string> DeleteBasicMaterialCategory(int basicMaterialCategoryId);
        public Task<string> UpdateBasicMaterialCategory(BasicMaterialCategories basicMaterialCategories);
    }
}
