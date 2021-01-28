using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreManyToMany
{
    public class EmployeeProject
    {
        public Guid EmployeesId { get; set; }
        public virtual Employee Employee { get; set; }

        public Guid ProjectsId { get; set; }
        public virtual Project Project { get; set; }

        public string Role { get; set; }
    }
}
