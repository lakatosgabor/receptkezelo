using System.ComponentModel.DataAnnotations;

namespace firstapp.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<Ingredients> Ingredients { get; set; }
    }
}
