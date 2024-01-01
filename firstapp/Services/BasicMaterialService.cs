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

        /// <summary>
        /// Alapanyag alap adatainak mentése
        /// </summary>
        public async Task<string> SaveBasicMaterial(BasicMaterials basicMaterials)
        {
            try
            {
                _recipeDbContext.BasicMaterials.Add(basicMaterials);
                int affectedRows = await _recipeDbContext.SaveChangesAsync();

                if (affectedRows <= 0)
                {
                    throw new InvalidOperationException("Sikertelen mentés!");
                }
                return "Mentés sikeres!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.InnerException.Message}");
            }
        }

        /// <summary>
        /// Alapanyag entitás törlése
        /// </summary>
        public async Task<string> DeleteBasicMaterial(int basicMaterialId)
        {
            try
            {
                var basicMaterial = _recipeDbContext.BasicMaterials.FirstOrDefault(e => e.Id == basicMaterialId);

                if (basicMaterial == null)
                {
                    throw new InvalidOperationException("Az alapanyag nem található az adatbázisban.");
                }

                var isUsedInOtherEntity = _recipeDbContext.IngredientsAllergens.Any(e => e.IngredientId == basicMaterialId);

                if (isUsedInOtherEntity)
                {
                    throw new InvalidOperationException("Az alapanyag más entitásokhoz kapcsolódik, nem törölhető.");
                }

                basicMaterial.IsDeleted = true;
                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres törlés!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.Message}");
            }
        }

        /// <summary>
        /// Alapanyag entitás szerkesztése
        /// </summary>
        public async Task<string> UpdateBasicMaterial(BasicMaterials basicMaterials)
        {
            try
            {
                var existingBasicMaterial = await _recipeDbContext.BasicMaterials.FirstOrDefaultAsync(r => r.Id == basicMaterials.Id);
                if (existingBasicMaterial == null)
                {
                    throw new InvalidOperationException("Az alapanyag nem található az adatbázisban.");
                }

                existingBasicMaterial.Name = basicMaterials.Name;
                existingBasicMaterial.BasicMaterialCategoryId = basicMaterials.BasicMaterialCategoryId;

                await _recipeDbContext.SaveChangesAsync();
                return "Sikeres módosítás!";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Hiba történt a mentés közben: {ex.InnerException.Message}");
            }

        }
    }
}
