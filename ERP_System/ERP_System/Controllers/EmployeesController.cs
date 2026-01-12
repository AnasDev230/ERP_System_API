using ERP_System.Models.Domain;
using ERP_System.Models.DTO.Employee;
using ERP_System.Repositories.Implementation;
using ERP_System.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IEmployeeRepository employeeRepository;

        public EmployeesController(UserManager<IdentityUser> userManager, IEmployeeRepository employeeRepository)
        {
            this.userManager = userManager;
            this.employeeRepository = employeeRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.Phone,

            };
            var identityResult = await userManager.CreateAsync(identityUser, "123@123");
            if (identityResult.Succeeded)
            {
                var employee = new Employee
                {
                    UserId = identityUser.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    HireDate = request.HireDate,
                    Salary = request.Salary,
                };
                employee = await employeeRepository.CreateEmployeeAsync(employee);
                return Created("", "Employee Created Successfully");

            }
            return BadRequest("Something went wrong!!");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await employeeRepository.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound("Employee Not Found!!");
            }
            return Ok(new
            {
                employee.Id,
                employee.FirstName,
                employee.LastName,
                employee.User.Email,
                employee.User.PhoneNumber,
                employee.HireDate,
                employee.Salary,
                Attendances = employee.Attendances.Select(a => new
                {
                    a.Id,
                    a.Date,
                    a.Status,
                    a.CheckInTime,
                    a.CheckOutTime
                }),

                Payrolls = employee.Payrolls.Select(p => new
                {
                    p.Id,
                    p.PayDate,
                    p.BasicSalary,
                    p.Allowances,
                    p.Deductions,
                    NetSalary = p.NetSalary,
                    p.Note
                })
            }
                );
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] string? firstName, [FromQuery] decimal? salary,
                                              [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var (employees,totalCount) = await employeeRepository.GetEmployeesByFilterAsync(firstName, salary, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            if (!employees.Any())
                return NotFound("No Employees!");
            return Ok(new
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalCount,
                data=employees.Select(e => new
                {
                    EmployeeId = e.Id,
                    FullName=e.FirstName+ " " + e.LastName,
                    HireDate=e.HireDate,
                    Salary=e.Salary,
                    Email=e.User.Email,
                    PhoneNumber= e.User.PhoneNumber,
                })
            }
                );
        }
    }
    }
