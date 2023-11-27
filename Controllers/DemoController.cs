using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Runtime.Intrinsics.X86;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Web_API_Tutorials_.Net_Core_7_C_.MyLogging;


namespace Web_API_Tutorials_.Net_Core_7_C_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //In software engineering, dependency injection is a programming technique in which an object or function receives
        //other objects or functions that it requires, as opposed to creating them internally.Dependency injection aims to
        //separate the concerns of constructing objects and using them, leading to loosely coupled programs.
        //The pattern ensures that an object or function which wants to use a given service should not have to
        //know how to construct those services.Instead, the receiving 'client' (object or function) is provided with its
        //dependencies by external code(an 'injector'), which it is not aware of.Dependency injection makes implicit
        //dependencies explicit and helps solve the following problems:

        //How can a class be independent from the creation of the objects it depends on?
        //How can an application, and the objects it uses support different configurations?
        //How can the behavior of a piece of code be changed without editing it directly?

        // Müssen in den StudentController zur passenden HTTP Methode reinkopiert werden

        // 1.Loosely coupled 
        private readonly IMyLogger _myLogger;

        // added attributes
        public DemoController(IMyLogger myLogger)
        {
            _myLogger = myLogger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("Index method started");

            return Ok();
        }

        // 2.Strongly coupled/thightly coupled
    }
}
