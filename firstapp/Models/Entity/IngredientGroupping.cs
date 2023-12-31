using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstapp.Models.Entity
{
    public class IngredientGroupping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public Ingredients Ingredients { get; set; }

        public int IngredientGroupId { get; set; }
        [ForeignKey("IngredientGroupId")]
        public IngredientGroups IngredientGroups { get; set; }
    }
}
