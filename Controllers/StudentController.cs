using Microsoft.AspNetCore.Mvc;
using Web_API_Tutorials_.Net_Core_7_C_.Models;

// creating controller über rechtsklick controller add controller und dann api empty wählen

namespace Web_API_Tutorials_.Net_Core_7_C_.Controllers
{
    // enable route
    [Route("api/[controller]")]
    // enable api controller
    [ApiController]

    //enable class as controller class : inherit from
    public class StudentController : ControllerBase
    {
        // http attribut
        [HttpGet]
        // build endpoint
        // modify to enum
        // fill in namespace for Students
        public IEnumerable<Student> GetStudents()
        {
            // Datensätze in CollegeRepository gecuttet
            // return auf "CollegeRepository" geändert
            return CollegeRepository.Students;
        }

        // weitere suchfunktion eingefügt

        // routing eingeführt
        [HttpGet("{id:int}")]
        public Student GetStudentById(int id)
        {
            // return type geändert
            return CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
        }
    }
}
