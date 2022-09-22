using Microsoft.AspNetCore.Mvc;
using OrganisationAPI.Models;

namespace OrganisationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentToEmployeeController : ControllerBase
    {
        private OrganisationdbContext _context;
        public DepartmentToEmployeeController(OrganisationdbContext organisationContext)
        {
            _context = organisationContext;
        }
        [HttpGet("/GetDepartmentToEmployee/{ID}")]
        public DepartmentToEmployee GetDepartmentToEmployee(int ID)
        {
            return _context.DepartmentToEmployees.FirstOrDefault(x => x.Id == ID);
        }
        [HttpGet("/GetDepartmentsToEmployees")]
        public IEnumerable<DepartmentToEmployee> GetDepartmentsToEmployees()
        {
            return _context.DepartmentToEmployees;
        }
        [HttpPost("/CreateDepartmentToEmployee/")]
        public DepartmentToEmployee CreateDepartmentToEmployee(DepartmentToEmployee departmentToEmployee)
        {
            DepartmentToEmployee newDepartmentToEmployee = new DepartmentToEmployee()
            {
                DepartmentId = departmentToEmployee.DepartmentId,
                EmployeeId = departmentToEmployee.EmployeeId
            };
            _context.DepartmentToEmployees.Add(newDepartmentToEmployee);
            _context.SaveChanges();
            return newDepartmentToEmployee;
        }
        [HttpPut("/UpdateDepartmentToEmployee/")]
        public DepartmentToEmployee UpdateDepartmentToEmployee(DepartmentToEmployee departmentToEmployee)
        {
            DepartmentToEmployee oldDepartmentToEmployee = _context.DepartmentToEmployees.FirstOrDefault(x => x.Id == departmentToEmployee.Id);
            oldDepartmentToEmployee.EmployeeId = departmentToEmployee.EmployeeId;
            oldDepartmentToEmployee.DepartmentId = departmentToEmployee.DepartmentId;
            _context.SaveChanges();
            return oldDepartmentToEmployee;
        }
        [HttpDelete("/DeleteDepartmentToEmployee/{ID}")]
        public void DeleteDepartmentToEmployee(int ID)
        {
            DepartmentToEmployee departmentToEmployee = _context.DepartmentToEmployees.FirstOrDefault(x => x.Id == ID);
            if (departmentToEmployee != null)
            {
                _context.DepartmentToEmployees.Remove(departmentToEmployee);
                _context.SaveChanges();
            }
        }
    }
}
