using System;
using System.Collections.Generic;

namespace OrganisationAPI.Models
{
    public partial class DepartmentToEmployee
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
    }
}
