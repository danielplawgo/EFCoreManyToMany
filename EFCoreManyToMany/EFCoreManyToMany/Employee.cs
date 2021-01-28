using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreManyToMany
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
