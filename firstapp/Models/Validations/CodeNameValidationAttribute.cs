using System;
using System.ComponentModel.DataAnnotations;

namespace firstapp.Models.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class CodeNameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string codeName = value.ToString();

                // Kódneve legalább 6, legfeljebb 12 karakterből áll
                if (codeName.Length < 6 || codeName.Length > 12)
                {
                    return new ValidationResult("A kódnévnek legalább 6, de legfeljebb 12 karakterből kell állnia.");
                }

                // Csak számokat vagy nagy betűket tartalmazhat
                if (!System.Text.RegularExpressions.Regex.IsMatch(codeName, "^[0-9A-Z]*$"))
                {
                    return new ValidationResult("A kódnév csak számokat vagy nagy betűket tartalmazhat.");
                }

                // Nem kezdődhet számmal
                if (char.IsDigit(codeName[0]))
                {
                    return new ValidationResult("A kódnév nem kezdődhet számmal.");
                }

                // Nem tartalmazhat szóközt és csak specifikus speciális karaktereket
                if (codeName.Contains(" ") || !codeName.All(c => char.IsLetterOrDigit(c) || c == '_' || c == '!'))
                {
                    return new ValidationResult("A kódnév nem tartalmazhat szóközt és csak számokat, nagy betűket, alulvonást (_) vagy felkiáltójelet (!) tartalmazhat.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
