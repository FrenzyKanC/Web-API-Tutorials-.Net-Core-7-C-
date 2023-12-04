using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web_API_Tutorials_.Net_Core_7_C_.Data.Config
{
    // inherit : interface
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);
           // id
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.StudentName).IsRequired();
            builder.Property(n => n.StudentName).HasMaxLength(250);
            builder.Property(n => n.StudentName).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.StudentName).IsRequired(true).HasMaxLength(250);

            builder.HasData(new List<Student>()
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
        }
        // added hasdata method to builder (default inhalt)
    }
}
