using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreManyToMany
{
    public class Project
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
