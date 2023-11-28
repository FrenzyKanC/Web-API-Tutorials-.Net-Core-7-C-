// code 1st approach

using Microsoft.EntityFrameworkCore;

namespace Web_API_Tutorials_.Net_Core_7_C_.Data
{
    // wird durch : DbContext zur passenden class
    // db inside the entity framework
    public class CollegeDBContext : DbContext
    {
        DbSet<Student> Students { get; set; }   
    }
}
