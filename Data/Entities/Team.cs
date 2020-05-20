using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Data.Entities
{
    public class Team
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Employee teamLeader { get; set; }

        public List<Employee> teamMembers { get; set; }
    }
}
