using Microsoft.AspNetCore.Identity;

namespace ERP_System.Models.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
    }
}
