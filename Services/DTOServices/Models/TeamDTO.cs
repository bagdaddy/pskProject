using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Services.DTOServices.Models
{
    public class TeamDTO
    {
        public Guid Id { get; set; }
        public EmployeeDTO teamLeader { get; set; }
        public List<EmployeeDTO> teamMembers { get; set; }

    }
}
