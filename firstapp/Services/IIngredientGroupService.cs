using firstapp.Models.Entity;

namespace firstapp.Services
{
    public interface IIngredientGroupService
    {
        public Task<string> SaveIngredientGroup(IngredientGroups ingredientGroup);
        public Task<string> DeleteIngredientGroup(int ingredientGroupId);
        public Task<string> UpdateIngredientGroup(IngredientGroups ingredientGroup);
    }
}
