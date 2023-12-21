using System.ComponentModel.DataAnnotations;

namespace firstapp.Models
{
    public class Allergens
    {
        [Key]
        public int AllergenId { get; set; }
        public string Name { get; set; }
        public List<Ingredients> Ingredients { get; set; }
    }
}
