using Microsoft.AspNetCore.Mvc;
using OrganisationAPI.Models;

namespace OrganisationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecretController : ControllerBase
    {
        private OrganisationdbContext _context;
        public SecretController(OrganisationdbContext organisationContext)
        {
            _context = organisationContext;
        }
        [HttpDelete("/ClearDataBase")]
        public void ClearDataBase()
        {
            _context.DepartmentToEmployees.RemoveRange(_context.DepartmentToEmployees);
            _context.SaveChanges();
            _context.Departments.RemoveRange(_context.Departments);
            _context.Employees.RemoveRange(_context.Employees);
            _context.SaveChanges();
        }
    }
}
