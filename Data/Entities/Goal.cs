using System;
namespace TP.Data.Entities
{
    public class Goal
    {
        public Guid Id { get; set; }
        public Employee Employee { get; set; }
        public Subject Subject { get; set; 
        public Goal(Employee employee, Subject subject)
        {
            Id = Guid.NewGuid();
            Employee = employee;
            Subject = subject;
        }
    }
}