using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firstapp.Models.Entity
{
    public class BasicMaterials : AbstractEntity
    {
        [Required]
        public string Name { get; set; }

        public int BasicMaterialCategoryId { get; set; }

        [ForeignKey("BasicMaterialCategoryId")]
        public BasicMaterialCategories BasicMaterialCategories { get; set; }
    }
}
