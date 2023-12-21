using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace firstapp.Models
{
    public class Recipes
    {
        [Key]
        public int RecipeId { get; set; }
        public string CodeName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IngredientGroups> IngredientGroups { get; set; }
    }
}
