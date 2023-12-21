using System.ComponentModel.DataAnnotations;

namespace firstapp.Models
{
    public class IngredientGroups
    {
        [Key]
        public int IngredientGroupId { get; set; }
        public string GroupName { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        public int RecipeId { get; internal set; }
    }
}
