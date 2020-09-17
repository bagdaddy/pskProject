using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Data.Entities
{
    public class Employee  : IdentityUser<Guid>
    {
        public Guid? BossId { get; set; }
        public int GlobalDayLimit { get; set; } = 3;
        public int? LocalDayLimit { get; set; } = null;
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public List<EmployeeSubject> LearnedSubjects { get; set; }
        public List<Employee> Subordinates { get; set; } = new List<Employee>();
        public List<Goal> Goals { get; set; }
        public List<Invite> Invites { get; set; }
        [ForeignKey("UserRoleId")]
        public virtual UserRole UserRole { get; set; }
        public Guid? UserRoleId { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
