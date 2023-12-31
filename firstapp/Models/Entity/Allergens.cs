using System.ComponentModel.DataAnnotations;

namespace firstapp.Models.Entity
{
    public class Allergens : AbstractEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
