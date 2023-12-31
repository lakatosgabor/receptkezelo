using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstapp.Models.Entity
{
    public class Ingredients : AbstractEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal? Quantity { get; set; }
        [Required]
        public string Unit { get; set; }

        public int BasicMaterialId { get; set; }

        [ForeignKey("BasicMaterialId")]
        public BasicMaterials BasicMaterials { get; set; }
    }
}
