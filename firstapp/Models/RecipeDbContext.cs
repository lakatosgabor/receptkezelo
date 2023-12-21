using Microsoft.EntityFrameworkCore;

namespace firstapp.Models
{
    public class RecipeDbContext : DbContext
    {

        public RecipeDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<IngredientGroups> IngredientGroups { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Allergens> Allergens { get; set; }
        public DbSet<Categories> Categories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Recipes>()
            .HasMany(r => r.IngredientGroups)
            .WithOne()
            .HasForeignKey(ig => ig.RecipeId);

            modelBuilder.Entity<IngredientGroups>()
                .HasMany(ig => ig.Ingredients)
                .WithOne()
                .HasForeignKey(i => i.IngredientGroupId);

            modelBuilder.Entity<Ingredients>()
                .HasOne(i => i.Categories)
                .WithMany(c => c.Ingredients)
                .HasForeignKey(i => i.CategoryId);

            modelBuilder.Entity<Ingredients>()
                .HasMany(i => i.Allergens)
                .WithMany(a => a.Ingredients)
                .UsingEntity(j => j.ToTable("IngredientAllergen"));

            modelBuilder.Entity<Categories>()
                .HasMany(c => c.Ingredients)
                .WithOne(i => i.Categories)
                .HasForeignKey(i => i.CategoryId);
        }
    }
}
