using Microsoft.AspNetCore.Mvc;

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
        public string GetStudentName()
        {
            return "Student name 1";
        }
    }
}
