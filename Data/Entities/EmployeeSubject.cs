using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Data.Entities
{
    public class EmployeeSubject
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
