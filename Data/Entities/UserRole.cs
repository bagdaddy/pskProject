using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Data.Entities
{
    public class UserRole
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public List<Employee> EmployeeList { get; set; }

        public UserRole (string Name)
        {
            this.Name = Name;
            this.Id = Guid.NewGuid();
            this.EmployeeList = new List<Employee>();
        }
    }
}
