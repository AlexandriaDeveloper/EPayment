namespace AlexFacultyOfMed.Models
{
    public class DailyFileDetails
    {
        public int Id { get; set; }

        public decimal Net { get; set; }
        public int DailyFileId { get; set; }

        public int EmployeeId { get; set; }
        public DailyFile DailyFile { get; set; }

        public Employee Employee { get; set; }
    }
}