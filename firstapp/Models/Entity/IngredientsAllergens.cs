using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstapp.Models.Entity
{
    public class IngredientsAllergens
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public Ingredients Ingredients { get; set; }

        public int AllergenId { get; set; }
        [ForeignKey("AllergenId")]
        public Allergens Allergens { get; set; }
    }
}
