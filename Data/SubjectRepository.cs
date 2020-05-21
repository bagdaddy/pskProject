using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Contexts;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Data
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SubjectContext _context;
        public SubjectRepository(SubjectContext context)
        {
            _context = context;
        }

        public void AddSubject(Subject subject)
        {
            _context.Add(subject);
        }

        public void Delete(Subject subject)
        {
            _context.Remove(subject);
        }

        public List<Subject> GetAll()
        {
            var subjectList = _context.Subjects.AsNoTracking().Include(x => x.ChildSubjects).Where(x => x.ParentSubjectId == null).ToList();
            return subjectList;
        }

        public Subject GetById(Guid id)
        {
            return _context.Subjects.FirstOrDefault(x => x.Id == id);
        }

        public Subject GetByIdWithChild(Guid id)
        {
            return _context.Subjects.Include(x => x.ChildSubjects).FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void UpdateSubjects(Subject subject)
        {
            _context.Subjects.Update(subject);
        }
    }
}
