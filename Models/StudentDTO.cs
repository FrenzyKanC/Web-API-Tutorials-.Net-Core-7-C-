using System.ComponentModel.DataAnnotations;

namespace Web_API_Tutorials_.Net_Core_7_C_.Models
// one way of reducing the number of calls is to use an object (the DTO) that aggregates the data
// that would have been transferred by the several calls, but that is served by one call only
{
    public class StudentDTO
    {
        // added Requirements
        // funktioniert nur mit dem [ApiController] aus dem StudentController, zeile 15
        public int Id { get; set; }

        [Required]
        public string StudentName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Adress { get; set; }
    }
}
