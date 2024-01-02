using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using firstapp.Models.Validations;

namespace firstapp.Models.Entity
{
    public class Recipes : AbstractEntity
    {
        [Required]
        [CodeNameValidation(ErrorMessage = "Hibás kódnév formátum.")]
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
