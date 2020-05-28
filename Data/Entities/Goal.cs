using System;
namespace TP.Data.Entities
{
    public class Goal
    {
        protected Goal() { }
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid SubjectId { get; set; }
        public Employee Employee { get; set; }
        public Subject Subject { get;  set; }
        public Goal(Guid employee, Guid subject)
        {
            Id = Guid.NewGuid();
            EmployeeId = employee;
            SubjectId = subject;
        }
    }
}