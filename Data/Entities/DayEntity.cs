using System;
using System.Collections.Generic;

namespace TP.Data.Entities
{
    public class DayEntity
    {
        public Guid Id { get; set; }
        public Guid EmployeesId { get; set; }
        public DateTime Date { get; set; }
        public List<Subject> SubjectList { get; set; }
    }
}
