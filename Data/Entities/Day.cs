using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP.Data.Entities
{
    public class Day
    {

        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public List<Guid> SubjectsId { get; set; }
        public List<Subject> SubjectList { get; set; }

    }
}