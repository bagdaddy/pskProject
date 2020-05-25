using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Data.Entities
{
    public class Team
    {
        public Guid Id { get; set; }
        public List<Team> ChildTeams { get; set; }
        public Employee TeamLeader { get; set; }
        public List<Employee> TeamMembers { get; set; }
    }
}
