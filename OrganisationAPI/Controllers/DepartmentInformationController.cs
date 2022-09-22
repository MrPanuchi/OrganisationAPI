using Microsoft.AspNetCore.Mvc;
using OrganisationAPI.Models;
using OrganisationAPI.Models.OtherModels;

namespace OrganisationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentInformationController : ControllerBase
    {
        private OrganisationdbContext _context;
        public DepartmentInformationController(OrganisationdbContext organisationContext)
        {
            _context = organisationContext;
        }
        [HttpGet("/GetDepartmentInformations")]
        public IEnumerable<DepartmentInformation> GetDepartmentInformations()
        {
            List<DepartmentInformation> result = new List<DepartmentInformation>();

            List<int> ids = _context.Departments.Select(x => x.Id).ToList();
            foreach (int id in ids)
            {
                result.Add(GetDepartmentInformationByID(id));
            }

            return result;
        }
        [HttpGet("/GetDepartmentInformation/{ID}")]
        public DepartmentInformation GetDepartmentInformationByID(int ID)
        {
            Department department = _context.Departments.FirstOrDefault(x => x.Id == ID);
            if (department == null)
            {
                return null;
            }
            List<int> employeeIDs = _context.DepartmentToEmployees.Where(x => x.DepartmentId == ID).Select(x => x.EmployeeId).ToList();
            List<string> posts = _context.Employees.Where(x => employeeIDs.Contains(x.Id)).Select(x => x.Post).ToList();

            return new DepartmentInformation()
            {
                Name = department.Name,
                EmployeeCount = employeeIDs.Count(),
                PostCount = posts.Distinct().Count()
            };
        }
    }
}
