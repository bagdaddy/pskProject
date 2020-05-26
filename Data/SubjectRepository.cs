using Microsoft.AspNetCore.Mvc;
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

        public Task<List<Subject>> GetAll()
        {
            var subjectList = _context.Subjects
                                .AsNoTracking()
                                .ToListAsync();
            return subjectList;
        }

        public Task<Subject> GetById(Guid id)
        {
            return  _context.Subjects
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Subject> GetByIdWithChild(Guid id)
        {
            return _context.Subjects
                .Include(x => x.ChildSubjects)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateSubjects(Subject subject)
        {
            _context.Subjects.Update(subject);
        }
        public Task<List<Subject>> GetChildSubjects(Guid id)
        {
            var subjectList = _context.Subjects
                .Include(x => x.ChildSubjects)
                .Where(x => x.ParentSubjectId.HasValue && x.ParentSubjectId.Value == id)
                .AsNoTracking()
                .ToListAsync();
            return subjectList;
        }
    }
}
