using firstapp.Models.Entity;

namespace firstapp.Services
{
    public interface IBasicMaterialCategoryService
    {
        public IQueryable<BasicMaterialCategories> GetBasicMaterialCategories(bool containDeleted);
    }
}
