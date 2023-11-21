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
        // added response documentation
        [ProducesResponseType(StatusCodes.Status200OK)]
        // alternative ohne den Typ <Student> bei den div. ActionResult:
        // [ProducesResponseType(200, Type = typeof(Student)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        // build endpoint
        // modify to enum
        // fill in namespace for Students
        // added ActionResult
        // obj auf StudentDTO geändert
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            // added DTO
            // added linq query syntax for non boolean http calls, statt foreach beispiel
            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Adress = s.Adress,
                Email = s.Email
            });

            // Datensätze in CollegeRepository gecuttet
            // return auf "CollegeRepository" geändert
            // added Ok -> StatusCode: 200 Succes
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // geändert auf single record, enum entfernt da einzelne person gesucht wird
        // added DTO
        public ActionResult<StudentDTO> GetStudentById(int id)
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
            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Adress = student.Adress,
                Email = student.Email
            };
            // return type geändert
            return Ok(studentDTO);
        }

        // added more Http Actions
        // alternative schreibweise für die route
        // constraint alph = alphabet; wegen http verb nicht string nimmt
        [HttpGet("{name:alph}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            // added StatusCode: 400 Bad Request Client Error
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (student == null)
                // added Errormessage
                return NotFound($"Student with id {name} not found!");
            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Adress = student.Adress,
                Email = student.Email
            };
            return Ok(studentDTO);
        }

        // added new method
        [HttpPost]
        // pfad: api/student/create
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        // Daten könne vom Body: [FromBody] oder vom querry: ?
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
            if(model == null)
                return BadRequest();
            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Adress = model.Adress,
                Email = model.Email
            };
            CollegeRepository.Students.Add(student);

            model.Id = student.Id;

            return Ok(student);
        }

        // restrticted range of id --> siehe list of constraints
        [HttpDelete("{id:min(1):max(100)}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
