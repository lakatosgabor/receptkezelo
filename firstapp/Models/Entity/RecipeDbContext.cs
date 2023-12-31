using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace firstapp.Models.Entity
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions options) : base(options)
        {
            Database.SetCommandTimeout(60);
        }

        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<IngredientGroups> IngredientGroups { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Allergens> Allergens { get; set; }
        public DbSet<BasicMaterialCategories> BasicMaterialCategories { get; set; }
        public DbSet<BasicMaterials> BasicMaterials { get; set; }
        public DbSet<IngredientGroupping> IngredientGroupping { get; set; }
        public DbSet<IngredientsAllergens> IngredientsAllergens { get; set; }
        public DbSet<RecipeIngredients> RecipeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.RemovePluralizingTableNameConvention();
            modelBuilder.RemoveOneToManyCascadeDeleteConvention();

            base.OnModelCreating(modelBuilder);
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.BaseType == null && !HasAttribute(entity.ClrType, typeof(TableAttribute)))
                {
                    entity.SetTableName(entity.DisplayName());
                }
            }
        }

        public static void RemoveOneToManyCascadeDeleteConvention(this ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }

        private static bool HasAttribute(Type type, Type attributeType)
        {
            return type.GetCustomAttribute(attributeType) != null;
        }
    }
}
