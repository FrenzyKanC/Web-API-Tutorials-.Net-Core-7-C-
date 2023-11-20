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
        // added ActionResult
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            // Datensätze in CollegeRepository gecuttet
            // return auf "CollegeRepository" geändert
            // added StatusCode: 200 Succes
            return Ok(CollegeRepository.Students);
        }

        // weitere suchfunktion eingefügt, Http Verbs angepasst
        // () aufgefüllt mit datentyp und name

        [HttpGet]
        // dynamic value {id} route
        // , Name = "GetStudentById" ist optional
        // added routing constraint (id:int) um multiple anwahlmöglichkeiten,durch einen datentyp,
        // bei einem request zu verhindern
        [Route("{id:int}", Name = "GetStudentById")]
        // geändert auf single record, enum entfernt da einzelne person gesucht wird
        public ActionResult<Student> GetStudentById(int id)
        {
            // added StatusCode: 400 Bad Request Client Error
            if (id <= 0)
                return BadRequest();

            // abfang falsche !nicht vorhandener! id
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            // added StatusCode: 404 Not Found Client Error
            if (student == null)
                // added Errormessage
                return NotFound($"Student with id {id} not found!");

            // return type geändert
            return Ok(student);
        }

        // added more Http Actions
        // alternative schreibweise für die route
        // constraint alph = alphabet; wegen http verb nicht string nimmt
        [HttpGet("{name:alph}", Name = "GetStudentByName")]
        public ActionResult<Student> GetStudentByName(string name)
        {
            // added StatusCode: 400 Bad Request Client Error
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (student == null)
                // added Errormessage
                return NotFound($"Student with id {name} not found!");
            return Ok(student);
        }

        // restrticted range of id --> siehe list of constraints
        [HttpDelete("{id:min(1):max(100)}", Name = "DeleteStudentById")]
        public ActionResult<bool> DeleteStudent(int id)
        {
            // added StatusCode: 400 Bad Request Client Error
            if (id <= 0)
                return BadRequest();

            // abfang falsche !nicht vorhandener! id
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            // added StatusCode: 404 Not Found Client Error
            if (student == null)
                // added Errormessage
                return NotFound($"Student with id {id} not found!");
            // return type geändert
            return Ok(student);
            // added remove
            CollegeRepository.Students.Remove(student);
            return true;
        }
    }
}
