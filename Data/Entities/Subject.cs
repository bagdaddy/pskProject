using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Data.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Subject ParentSubject { get; set; }

        public string Description { get; set; }

        public Subject(string name, Subject subject, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            if(subject != null)
            {
                ParentSubject = subject;
            }
            Description = description;
        }
        public Subject(Guid id, string name, Subject subject, string description)
        {
            Id = id;
            Name = name;
            if (subject != null)
            {
                ParentSubject = subject;
            }
            Description = description;
        }
    }
}
