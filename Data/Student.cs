// entitiy framework - entity first

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API_Tutorials_.Net_Core_7_C_.Data
{
    // entity class
    public class Student
    {
        // Id ist hierdurch der primary key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
    }
}
