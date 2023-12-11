// fixed errors die durch das löschen der models (in-memory repositories) entstanden sind
// removed in memory repositories with framework _dbContext

// how create in ef
// 1. create obj for entitiy class
// 2. add obj to table
// 3. save changes


using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API_Tutorials_.Net_Core_7_C_.Data;
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
        // using in-build logger
        private readonly ILogger<StudentController> _logger;

        // using entity framework
        private readonly CollegeDBContext _dbContext;
        
        // get instance from dependecy injection:
        public StudentController(ILogger<StudentController> logger, CollegeDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

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

        // added Async method -> await
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            // added logger
            _logger.LogInformation("GetStudents method started");
            // added DTO
            // added linq query syntax for non boolean http calls, statt foreach beispiel

            // return all records from db in Student Table:
            // var students = await _dbContext.Students.ToListAsync();

            // alternative: gibt nur diese werte wieder
            // DTOs geben dem programm "custom business logic" on top of the db
            
            var students = await _dbContext.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email,
                DOB = s.DOB,
            }).ToListAsync();

            // Datensätze in CollegeRepository gecuttet
            // return auf "CollegeRepository" geändert
            // added Ok -> StatusCode: 200 Succes
            return Ok(_dbContext.Students);
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
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }
            // abfang falsche !nicht vorhandener! id
            var student = _dbContext.Students.Where(n => n.Id == id).FirstOrDefault();
            // added StatusCode: 404 Not Found Client Error
            if (student == null)
            {
                _logger.LogError("Student not found with given Id");
                // added Errormessage
                return NotFound($"Student with id {id} not found!");
            }

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email,
                DOB = student.DOB
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
            var student = _dbContext.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (student == null)
                // added Errormessage
                return NotFound($"Student with id {name} not found!");
            var studentDTO = new StudentDTO()
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email,
                DOB = student.DOB
            };
            return Ok(studentDTO);
        }

        // added new method
        [HttpPost]
        // pfad: api/student/create
        [Route("Create")]
        // changed statsu code to 201
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // Daten könne vom Body: [FromBody] oder vom querry: ?
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
            /* manuelle validation richtig eingegebener user daten außerhalb des StudentDTO Obj
            ModelState ist Fehlernachricht
            if(!ModelState.IsValid)
                return BadRequest(ModelState); */

            if(model == null)
                return BadRequest();

            // added validation attribute
            // 1.Methode Directly adding error message to modelstate
            // 2.Methode Using custom attribute
           /* if (model.AdmissionDate < DateTime.Now)
            {
                ModelState.AddModelError("Admission Error", "Admission date must be greater than or equal to todays date");
                return BadRequest(ModelState);
            } */

            int newId = _dbContext.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                // id entfernt, da der wert über die DB kommt
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email,
                DOB = model.DOB
            };
            _dbContext.Students.Add(student);
            // save changes für externe db
            _dbContext.SaveChanges();

            model.Id = student.Id;
            // Status 201,
            // "GetStudentById" = Route
            // new {id} parameter benötigt object
            // new created student details = model
            return CreatedAtRoute("GetStudentById", new {id = model.Id}, model);
        }

        // added new http call: Put
        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudent([FromBody]StudentDTO model)
        {
            if(model == null || model.Id <= 0)
                BadRequest();

            // um daten aus dem entity framework upzudaten, müssen sie erst gefetcht werden
            // AsNoTracking um id diskrepanzen zu vermeiden
            var existingStudent = _dbContext.Students.AsNoTracking().Where(s => s.Id == model.Id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            // new record, update in die db
            var newRecord = new Student()
            {
                Id = existingStudent.Id,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email,
                DOB = model.DOB
            };
            _dbContext.Students.Update(newRecord);

            // danach update
            // auskommentiert, da in memory
            /* existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;
            existingStudent.DOB = Convert.ToDateTime(model.DOB); */
            // danach save
            _dbContext.SaveChanges();

            return NoContent();
        }

        // added new http call: Patch
        // VORTEIL GEGENÜBER PUT: die payload an den server wird auf das reduziert, was tatsächlich geändert wird
        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                BadRequest();

            var existingStudent = _dbContext.Students.Where(s => s.Id == id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address
            };

            patchDocument.ApplyTo(studentDTO, ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO.Address;
            _dbContext.SaveChanges();

            return NoContent();
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

            // variable um student auszuwählen
            var student = _dbContext.Students.Where(n => n.Id == id).FirstOrDefault();

            // abfang falsche !nicht vorhandener! id
            // added StatusCode: 404 Not Found Client Error
            if (student == null)              
                // added Errormessage
                return NotFound($"Student with id {id} not found!");

            // return type geändert
            return Ok(student);
            // added remove
            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
