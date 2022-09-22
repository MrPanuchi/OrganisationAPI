using Microsoft.AspNetCore.Mvc;
using OrganisationAPI.Models;

namespace OrganisationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private OrganisationdbContext _context;
        public EmployeeController(OrganisationdbContext organisationContext)
        {
            _context = organisationContext;
        }
        [HttpGet("/GetEmployees/{ID}")]
        public Employee GetEmployee(int ID)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == ID);
        }
        [HttpGet("/GetEmployees")]
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }
        [HttpPost("/CreateEmployee/")]
        public Employee CreateEmployee(Employee employee)
        {
            Employee newEmployee = new Employee()
            {
                Name = employee.Name,
                Post = employee.Post
            };
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return newEmployee;
        }
        [HttpPut("/UpdateEmployee/")]
        public Employee UpdateEmployee(Employee employee)
        {
            Employee oldEmployee = _context.Employees.FirstOrDefault(x => x.Id == employee.Id);
            oldEmployee.Name = employee.Name;
            oldEmployee.Post = employee.Post;
            _context.SaveChanges();
            return oldEmployee;
        }
        [HttpDelete("/DeleteEmployee/{ID}")]
        public void DeleteEmployee(int ID)
        {
            if (_context.DepartmentToEmployees.Where(x => x.EmployeeId == ID).FirstOrDefault() != null)
            {
                throw new Exception("Is ID for DepartmentToEmployees");
            }
            Employee employee = _context.Employees.FirstOrDefault(x => x.Id == ID);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
    }
}
