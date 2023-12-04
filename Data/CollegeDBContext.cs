// code 1st approach

// connect to sql data base via view server explorer
// über properties vom server im server explorer kann der connection string gefunden werden
// die infos für die "requirements for db connection" befinden sich im string
using Microsoft.EntityFrameworkCore;
using Web_API_Tutorials_.Net_Core_7_C_.Data.Config;

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

        // überschreiben um db aufzufüllen (default data)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // inhalt aus StudentConfig vermitteln
            // weitere tables werden hier geadddet
            modelBuilder.ApplyConfiguration(new StudentConfig());
        }
    }
}
