using System.ComponentModel.DataAnnotations;

namespace firstapp.Models.Entity
{
    public class IngredientGroups : AbstractEntity
    {
        [Required]
        public string GroupName { get; set; }
    }
}
