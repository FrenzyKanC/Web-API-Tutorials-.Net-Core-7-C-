using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Web_API_Tutorials_.Net_Core_7_C_.Validators;

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

        [Required(ErrorMessage = "Student name is required")]
        public string StudentName { get; set; }

        [EmailAddress(ErrorMessage = "Enter valid Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        // added custom DateCheck
        [DateCheck]
        public DateTime AdmissionDate { get; set; }
    }
}
