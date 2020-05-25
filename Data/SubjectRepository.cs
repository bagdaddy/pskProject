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
        private readonly AppDbContext _context;
        public SubjectRepository(AppDbContext context)
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
            var subjectList = _context.Subjects
                .AsNoTracking()
                .ToList();
            return subjectList;
        }

        public Subject GetById(Guid id)
        {
            return _context.Subjects
                .Include(s => s.EmployeesWhoLearnedIt)
                .ThenInclude(es => es.Employee)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == id);
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
        public List<Subject> GetChildSubjects(Guid id)
        {
            var subjectList = _context.Subjects
                .Include(x => x.ChildSubjects)
                .Where(x => x.ParentSubjectId.HasValue && x.ParentSubjectId.Value == id)
                .AsNoTracking()
                .ToList();
            return subjectList;
        }
    }
}
