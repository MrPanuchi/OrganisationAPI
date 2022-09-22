using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;
using OrganisationAPI.Models;

namespace OrganisationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private OrganisationdbContext _context;
        public DepartmentController(OrganisationdbContext organisationContext)
        {
            _context = organisationContext;
        }
        [HttpGet("/GetDepartments/{parentDepartmentID}")]
        public IEnumerable<Department> GetDepartment(int parentDepartmentID)
        {
            return _context.Departments.Where(x => x.ParentDepartmentId == parentDepartmentID);
        }
        [HttpGet("/GetDepartment/{ID}")]
        public Department GetDepartmentById(int ID)
        {
            return _context.Departments.FirstOrDefault(x => x.Id == ID);
        }
        [HttpGet("/GetDepartments")]
        public IEnumerable<Department> GetDepartments()
        {
            return _context.Departments;
        }
        [HttpPost("/CreateDepartment/")]
        public Department CreateDepartment(Department department)
        {
            Department newDepartment = new Department()
            {
                Name = department.Name,
                ParentDepartmentId = department.ParentDepartmentId,
            };
            _context.Departments.Add(newDepartment);
            _context.SaveChanges();
            return newDepartment;
        }
        [HttpPut("/UpdateDepartment/")]
        public Department UpdateDepartment(Department department)
        {
            Department oldDepartment = _context.Departments.FirstOrDefault(x => x.Id == department.Id);
            oldDepartment.ParentDepartmentId = department.ParentDepartmentId;
            oldDepartment.Name = department.Name;
            _context.SaveChanges();
            return oldDepartment;
        }
        [HttpDelete("/DeleteDepartment/{departmentID}")]
        public void DeleteDepartment(int departmentID)
        {
            if (_context.Departments.Where(x => x.ParentDepartmentId == departmentID).FirstOrDefault() != null)
            {
                throw new Exception("Is parent for other departments");
            }
            if (_context.DepartmentToEmployees.Where(x => x.DepartmentId == departmentID).FirstOrDefault() != null)
            {
                throw new Exception("Is departmentID for DepartmentToEmployees");
            }
            Department removedDepartment = _context.Departments.FirstOrDefault(x => x.Id == departmentID);
            if (removedDepartment != null)
            {
                _context.Departments.Remove(removedDepartment);
                _context.SaveChanges();
            }
        }
    }
}
