using System;
using System.Collections.Generic;

namespace OrganisationAPI.Models
{
    public partial class Employee
    {
        public Employee()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Post { get; set; } = null!;
    }
}
