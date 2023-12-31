using firstapp.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Services
{
    public class BasicMaterialService : IBasicMaterialService
    {
        private readonly RecipeDbContext _recipeDbContext;

        public BasicMaterialService(RecipeDbContext context)
        {
            _recipeDbContext = context;
        }

        private IQueryable<BasicMaterials> GetBasedOnContainDeleted(bool containDeleted)
        {
            return containDeleted ? _recipeDbContext.Set<BasicMaterials>().IgnoreQueryFilters() : _recipeDbContext.Set<BasicMaterials>();
        }

        /// <summary>
        /// Összes alapanyag lekérdezése
        /// </summary>
        public IQueryable<BasicMaterials> GetBasicMaterials(bool containDeleted)
        {
            return GetBasedOnContainDeleted(containDeleted);
        }

        /// <summary>
        /// Egy adott kategóriába tartozó összes alapanyag
        /// </summary>
        public IQueryable<object> GetBasicMaterialsWithCategory(bool containDeleted, int categoryId)
        {
            IQueryable<BasicMaterials> basicMaterialsQuery = GetBasedOnContainDeleted(containDeleted);

            var materialsWithCategoryQuery = basicMaterialsQuery
                .Join(_recipeDbContext.BasicMaterialCategories,
                    material => material.BasicMaterialCategoryId,
                    category => category.Id,
                    (material, category) => new { Material = material, Category = category })
                .Where(joined => joined.Category.Id == categoryId)
                .Select(joined => new
                {
                    BasicMaterial = joined.Material,
                    BasicMaterialCategory = joined.Category
                });

            return materialsWithCategoryQuery.Select(item => new
            {
                BasicMaterial = item.BasicMaterial,
                BasicMaterialCategory = item.BasicMaterialCategory
            });
        }


        /// <summary>
        /// Egy adott allergénhez tartozó összes alapanyag
        /// </summary>
        public IQueryable<object> GetBasicMaterialsWithAllergen(bool containDeleted, int allergenId)
        {
            IQueryable<BasicMaterials> basicMaterialsQuery = GetBasedOnContainDeleted(containDeleted);

            var materialsWithAllergenQuery = basicMaterialsQuery
        .Join(_recipeDbContext.IngredientsAllergens,
            material => material.Id,
            ingredientsAllergens => ingredientsAllergens.IngredientId,
            (material, ingredientsAllergens) => new { Material = material, IngredientsAllergens = ingredientsAllergens })
        .Join(_recipeDbContext.Allergens,
            joined => joined.IngredientsAllergens.AllergenId,
            allergen => allergen.Id,
            (joined, allergen) => new { joined.Material, joined.IngredientsAllergens, Allergen = allergen })
        .Where(joined => joined.Allergen.Id == allergenId)
        .Select(joined => new
        {
            BasicMaterial = joined.Material,
            IngredientsAllergens = joined.IngredientsAllergens,
            Allergen = joined.Allergen
        });

            return materialsWithAllergenQuery.Select(item => new
            {
                BasicMaterial = item.BasicMaterial,
                IngredientsAllergens = item.IngredientsAllergens,
                Allergen = item.Allergen
            });
        }
    }
}
