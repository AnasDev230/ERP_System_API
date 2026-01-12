using ERP_System.Data;
using ERP_System.Models.Domain;
using ERP_System.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ERP_System.Repositories.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SystemDbContext dbContext;
        public EmployeeRepository(SystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> GetEmployeeAsync(Guid employeeId)
        {
            return await dbContext.Employees
                 .AsNoTracking()
                 .Include(e => e.User)        
                .Include(e => e.Attendances) 
                .Include(e => e.Payrolls)
                .FirstOrDefaultAsync(e=>e.Id==employeeId);
        }

        public async Task<(IEnumerable<Employee>,int totalCount)> GetEmployeesByFilterAsync(
    string? firstName = null,
    decimal? salary = null,
    int pageNumber = 1,
    int pageSize = 10)
        {
            var query = dbContext.Employees
                .AsNoTracking()
                .Include(e => e.User)
                .Include(e => e.Attendances)
                .Include(e => e.Payrolls)
                .AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(e => e.FirstName.Contains(firstName.ToLower()));
            }

            if (salary.HasValue)
            {
                query = query.Where(e => e.Salary == salary.Value);
            }
            var totalCount = await query.CountAsync();

            var pagedEmployees = await query.OrderBy(e => e.Id)               
                         .Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize)
                         .ToListAsync();

            return (pagedEmployees,totalCount);
        }

      
    }
}
