using System.ComponentModel.DataAnnotations;

namespace firstapp.Models.Entity
{
    public class Recipes : AbstractEntity
    {
        [Required]
        public string CodeName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        private int _prepareTime = 0; // Alapértelmezett érték beállítása privát mezőként
        public int PrepareTime
        {
            get { return _prepareTime; } // Az érték visszaadása
            set { _prepareTime = value; } // Érték beállítása
        }
        [Required]
        public int? CookingTime { get; set; }
    }
}
