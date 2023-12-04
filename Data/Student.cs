// entitiy framework - entity first

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API_Tutorials_.Net_Core_7_C_.Data
{
    // entity class
    public class Student
    {
        // primary key entfernt da in der data/config die StudentConfig jetzt id = key hat
        // Identity Caller entfernt s.o.
      
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
    }
}
