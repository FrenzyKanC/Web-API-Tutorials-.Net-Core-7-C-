using Microsoft.AspNetCore.Mvc;
using Web_API_Tutorials_.Net_Core_7_C_.Models;

// creating controller über rechtsklick controller add controller und dann api empty wählen
// added Web Api Routing Grafik (Action/Verb/Route)


namespace Web_API_Tutorials_.Net_Core_7_C_.Controllers
{
    // enable route
    // dynamic routing enabled durch [controller]
    // static routing = ("api/Pfad")
    [Route("api/[controller]")]
    // enable api controller
    [ApiController]

    //enable class as controller class : inherit from
    public class StudentController : ControllerBase
    {
        // http attribut
        // alle verbs haben die route des [controllers]
        // für geändertes routes -> [Route("All")] nach dem Verb adden
        [HttpGet]
        // All = template, 
        // Name = "GetAllStudents" = routename
        [Route("All", Name = "GetAllStudents")]

        // build endpoint
        // modify to enum
        // fill in namespace for Students
        public IEnumerable<Student> GetStudents()
        {
            // Datensätze in CollegeRepository gecuttet
            // return auf "CollegeRepository" geändert
            return CollegeRepository.Students;
        }

        // weitere suchfunktion eingefügt, Http Verbs angepasst
        // () aufgefüllt mit datentyp und name

        [HttpGet]
        // dynamic value {id} route
        // , Name = "GetStudentById" ist optional
        [Route("{id}", Name = "GetStudentById")]
        // geändert auf single record, enum entfernt da einzelne person gesucht wird
        public Student GetStudentById(int id)
        {
            // return type geändert
            return CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
        }

        // added more Http Actions
        // alternative schreibweise für die route
        [HttpGet("{name}", Name = "GetStudentByName")]
        public Student GetStudentByName(string name)
        {
            return CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault(); ;
        }

        [HttpDelete("{id}", Name = "DeleteStudentById")]
        public bool DeleteStudent(int id)
        {
            //"var stu"
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            // added remove
            CollegeRepository.Students.Remove(student);
            return true;
        }
    }
}
