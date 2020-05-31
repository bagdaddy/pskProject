using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    public interface ISubjectControllerService
    {
        Task<List<Subject>> GetAllSubjects(Subject currentSubject, List<Subject> subjectListHierarchy);
    }
}
