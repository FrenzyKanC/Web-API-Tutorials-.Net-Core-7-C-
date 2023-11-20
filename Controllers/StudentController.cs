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
            return new List<Student>{
                new Student
                {
                    Id = 1,
                    StudentName = "Student 1",
                    Email = "studentmail1@gmail.com",
                    Adress = "Hyd, INDIA"
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Student 2",
                    Email = "studentmail2@gmail.com",
                    Adress = "Banglore, INDIA"
                }
            };
        }
    }
}
