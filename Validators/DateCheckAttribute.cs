using System.ComponentModel.DataAnnotations;
// added Validator folder (2. Methode) -> Custom
namespace Web_API_Tutorials_.Net_Core_7_C_.Validators
{
    public class DateCheckAttribute : ValidationAttribute
    {
        // ValidationResult muss dafür überschrieben werden
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;

            if (date < DateTime.Now)
            {
                return new ValidationResult("The date must be greater than or equal to todays date");
            }
            return ValidationResult.Success;
        }
    }
}
