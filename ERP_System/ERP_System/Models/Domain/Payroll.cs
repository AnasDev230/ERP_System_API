namespace ERP_System.Models.Domain
{
    public class Payroll
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

       public DateTime PayDate { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public string? Note { get; set; }
        public decimal NetSalary => BasicSalary + Allowances - Deductions;
    }
}
