using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Data.Entities
{
    public class Invite
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Email { get; set; }
        public Invite(string email, Guid employeeId)
        {
            Id = new Guid();
            Email = email;
            EmployeeId = employeeId;
        }
    }
}
