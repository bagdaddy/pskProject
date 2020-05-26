using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAll();
        Task<Subject> GetById(Guid id);
        Task<Subject> GetByIdWithChild(Guid id);
        void Delete(Subject subject);
        void AddSubject(Subject subject);
        Task SaveChanges();
        void UpdateSubjects(Subject subject);
        Task<List<Subject>> GetChildSubjects(Guid id);
    }
}
