using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Data.Entities
{
    public class Subject
    {
        protected Subject() { }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid? ParentSubjectId { get; private set; }
        public string Description { get; private set; }
        private readonly List<Subject> _childSubjects = new List<Subject>();
        public IReadOnlyList<Subject> ChildSubjects => _childSubjects;
        public Subject(string name, Guid? parentSubjectId, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            ParentSubjectId = parentSubjectId;
            Description = description;
        }
        public void AddChildSubjects(Subject childSubject)
        {
            _childSubjects.Add(childSubject);
        }
        public void DeleteChildSubjects(Subject childSubject)
        {
            _childSubjects.Remove(childSubject);
        }
        public void UpdateSubject(string name, string description)
        {
            Name = string.IsNullOrEmpty(name) ? Name : name;
            Description = string.IsNullOrEmpty(description) ? Description : description;
            
        }
    }
}
