namespace Web_API_Tutorials_.Net_Core_7_C_.Models
    // adding in memory Repository
{
    public class CollegeRepository
    {
        public static List<Student> Students {  get; set; } = new List<Student>() 
        {
                new Student
                {
                    Id = 1,
                    StudentName = "Student 1",
                    Email = "studentmail1@gmail.com",
                    Address = "Hyd, INDIA"
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Student 2",
                    Email = "studentmail2@gmail.com",
                    Address = "Banglore, INDIA"
                }
        };
    }
}
