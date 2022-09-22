using Microsoft.AspNetCore.Mvc;
using OrganisationAPI.Models;
using OrganisationAPI.Models.ImportModels;
using OrganisationAPI.Services.Interfaces;
using System.Text.RegularExpressions;

namespace OrganisationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportXLSXController : ControllerBase
    {
        private OrganisationdbContext _context;
        private IXLSXParser _xLSXParserService;
        private IImportNote _importNoteService;

        public ImportXLSXController(OrganisationdbContext context, IXLSXParser xLSXParser, IImportNote importNote)
        {
            _context = context;
            _xLSXParserService = xLSXParser;
            _importNoteService = importNote;
        }
        [HttpPost]
        public void ImportXLSXFile(IFormFile file)
        {
            if (IsCorrectFile(file))
            {
                string[][] data = _xLSXParserService.ParseXLSXToCSV(file);
                IEnumerable<ImportLetter> letters = _importNoteService.CreateImportLetters(data.Skip(1).SkipLast(1).ToArray());
                UpdateDataBase(letters);
            }
        }
        private void UpdateDataBase(IEnumerable<ImportLetter> importLetters)
        {
            foreach (ImportLetter importLetter in importLetters)
            {
                if(!CheckOnExistDepartment(importLetter))
                {
                    _context.Departments.Add(
                        new Department()
                        {
                            Name = importLetter.DepartmentName,
                            ParentDepartmentId = FindDepartmentID(importLetter.DepartmentParentName)
                        });
                    _context.SaveChanges();
                }
                if(!CheckOnExistEmployee(importLetter))
                {
                    _context.Employees.Add(
                        new Employee()
                        {
                            Name = importLetter.EmployeeName,
                            Post = importLetter.EmployeePost,
                        });
                    _context.SaveChanges();
                }
                if(!CheckOnExistDepartmentToEmployee(importLetter))
                {
                    _context.DepartmentToEmployees.Add(
                        new DepartmentToEmployee()
                        {
                            DepartmentId = FindDepartmentID(importLetter.DepartmentName),
                            EmployeeId = FindEmployeeID(importLetter.EmployeeName, importLetter.EmployeePost)
                        });
                    _context.SaveChanges();
                }
            }
            _context.SaveChanges();
        }
        private bool IsCorrectFile(IFormFile file)
        {
            Regex rx = new Regex("[.]xlsx$");

            if (!rx.IsMatch(file.FileName))
            {
                return false;
            }
            if (file.Length == 0)
            {
                return false;
            }
            return true;
        }
        private bool CheckOnExistDepartment(ImportLetter letter)
        {
            if (_context.Departments.Where
                (x => x.Name == letter.DepartmentName).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }
        private bool CheckOnExistEmployee(ImportLetter letter)
        {
            if (_context.Employees.Where
                (x => x.Name == letter.EmployeeName && x.Post == letter.EmployeePost).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }
        private bool CheckOnExistDepartmentToEmployee(ImportLetter letter)
        {
            Department department = _context.Departments.Where
                (x => x.Name == letter.DepartmentName).FirstOrDefault();
            if (department == null)
            {
                return false;
            }
            Employee employess = _context.Employees.Where
                (x => x.Name == letter.EmployeeName && x.Post == letter.EmployeePost).FirstOrDefault();
            if (employess == null)
            {
                return false;
            }
            DepartmentToEmployee departmentToEmployee = _context.DepartmentToEmployees.Where
                (x => x.DepartmentId == department.Id && x.EmployeeId == employess.Id).FirstOrDefault();
            if (departmentToEmployee == null)
            {
                return false;
            }
            return true;
        }
        private int FindDepartmentID(string departmentName)
        {
            if (departmentName == "")
            {
                return -1;
            }
            return _context.Departments.Where
                (x => x.Name == departmentName).FirstOrDefault().Id;
        }
        private int FindEmployeeID(string name, string post)
        {
            return _context.Employees.Where
                (x => x.Name == name && x.Post == post).FirstOrDefault().Id;
        }
    }
}
