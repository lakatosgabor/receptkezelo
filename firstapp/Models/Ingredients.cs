using System.ComponentModel.DataAnnotations;

namespace firstapp.Models
{
    public class Ingredients
    {
        [Key]
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public int CategoryId { get; set; }
        public Categories Categories { get; set; }
        public List<Allergens> Allergens { get; set; }
        public int IngredientGroupId { get; internal set; }
    }
}
