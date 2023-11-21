namespace Web_API_Tutorials_.Net_Core_7_C_.Models
// one way of reducing the number of calls is to use an object (the DTO) that aggregates the data
// that would have been transferred by the several calls, but that is served by one call only
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
    }
}
