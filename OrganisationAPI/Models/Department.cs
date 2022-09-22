using System;
using System.Collections.Generic;

namespace OrganisationAPI.Models
{
    public partial class Department
    {
        public Department()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? ParentDepartmentId { get; set; }
    }
}
