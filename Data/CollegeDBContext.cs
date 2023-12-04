// code 1st approach

// connect to sql data base via view server explorer
// über properties vom server im server explorer kann der connection string gefunden werden
// die infos für die "requirements for db connection" befinden sich im string
using Microsoft.EntityFrameworkCore;

namespace Web_API_Tutorials_.Net_Core_7_C_.Data
{
    // wird durch : DbContext zur passenden class
    // db inside the entity framework
    public class CollegeDBContext : DbContext
    {
        // added constuctor mit den sql server bezogenen daten (kürzel: ctor)
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options):base(options)
        {
            
        }
        DbSet<Student> Students { get; set; }

        // überschreiben um db aufzufüllen
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new List<Student>()
           {
            new Student {
                Id = 1,
                StudentName = "Venkat",
                Address = "India",
                Email = "venkat@gmail.com",
                DOB = new DateTime(2022, 12, 12)
            },
            new Student
            {
                Id = 1,
                StudentName = "Nehanth",
                Address = "India",
                Email = "nehanth@gmail.com",
                DOB = new DateTime(2022, 6, 12)
            }
           });

            // added schema: max chars for data immigration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(n => n.StudentName).IsRequired();
                entity.Property(n => n.StudentName).HasMaxLength(250);
                entity.Property(n => n.StudentName).IsRequired(false).HasMaxLength(500);
                entity.Property(n => n.StudentName).IsRequired(true).HasMaxLength(250);
            });
        }
    }
}
