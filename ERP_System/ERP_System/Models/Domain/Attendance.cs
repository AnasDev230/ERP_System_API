namespace ERP_System.Models.Domain
{
    public class Attendance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public DateTime Date { get; set; }
        public string Status { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
    }
}
