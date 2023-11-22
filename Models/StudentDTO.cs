using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Web_API_Tutorials_.Net_Core_7_C_.Models
// one way of reducing the number of calls is to use an object (the DTO) that aggregates the data
// that would have been transferred by the several calls, but that is served by one call only
{
    public class StudentDTO
    {
        // added Requirements
        // funktioniert nur mit dem [ApiController] aus dem StudentController, zeile 15
        [ValidateNever]
        public int Id { get; set; }

        // added ErrorMessage
        [Required(ErrorMessage = "Student name is required")]
        // added stringlenght
        [StringLength(30)]
        public string StudentName { get; set; }

        [EmailAddress(ErrorMessage = "Enter valid Email address")]
        public string Email { get; set; }

        // range validation
        public int Age { get; set; }
        [Range(10, 30)]

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }



        // passwort validator
        public string Password { get; set; }

        // vergleicht mit Password
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
