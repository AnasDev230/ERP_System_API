using ERP_System.Models.Domain;

namespace ERP_System.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeAsync(Guid employeeId);
        Task<(IEnumerable<Employee>, int totalCount)> GetEmployeesByFilterAsync(string? firstName = null,
            decimal? salary = null,
            int pageNumber = 1,
               int pageSize = 10);
    }
}
