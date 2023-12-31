using System.ComponentModel.DataAnnotations;

namespace firstapp.Models.Entity
{
    public class BasicMaterialCategories : AbstractEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
