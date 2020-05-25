using System;
namespace TP.Data.Entities
{
    public class Goal
    {
        public Guid Id { get; set; }
        public Guid EmployeesId { get; set; }
        public Guid SubjectId { get; set; }

        public Goal(Guid employeesid, Guid subjectId)
        {
            Id = Guid.NewGuid();
            EmployeesId = employeesid;
            SubjectId = subjectId;
        }
    }
}
