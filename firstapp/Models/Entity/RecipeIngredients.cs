using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstapp.Models.Entity
{
    public class RecipeIngredients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipes Recipes { get; set; }

        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public Ingredients Ingredients { get; set; }
    }
}
