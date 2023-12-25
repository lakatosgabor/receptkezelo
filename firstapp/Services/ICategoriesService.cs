using firstapp.Models;

namespace firstapp.Services
{
    public interface ICategoriesService
    {
        public IQueryable<Categories> GetCategories(bool containDeleted);

    }
}
