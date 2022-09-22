namespace OrganisationAPI.Models.ImportModels
{
    public class ImportLetter
    {
        public string DepartmentName { get; set; }
        public string DepartmentParentName { get; set; }
        public string EmployeePost { get; set; }
        public string EmployeeName { get; set; }
        public static ImportLetter CreateImportLetter (params string[] parameters)
        {
            if (parameters.Length == 4)
            {
                var note = new ImportLetter();
                note.DepartmentName = parameters[0];
                note.DepartmentParentName = parameters[1];
                note.EmployeePost = parameters[2];
                note.EmployeeName = parameters[3];
                return note;
            }
            else
            {
                throw new ArgumentException("parameters.Lenght must be 4");
            }
        }
    }
}
