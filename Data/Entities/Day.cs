using System;
using System.Collections.Generic;

namespace TP.Data.Entities
{
    public class Day
    {
        public Guid Id { get; set; }
        public Guid EmployeesId { get; set; }
        public DateTime Date { get; set; }
        public List<Subject> SubjectList { get; set; }

        public Day(Guid employeesid, DateTime date, List<Subject> subjectList)
        {
            Id = Guid.NewGuid();
            Date = date;
            EmployeesId = employeesid;
            SubjectList = subjectList;
        }
    }
}