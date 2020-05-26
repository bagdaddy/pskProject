using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Services
{
    public class SubjectControllerService : ISubjectControllerService
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectControllerService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<List<Subject>> GetAllSubjects( Subject currentSubject, List<Subject> subjectListHierarchy)
        {
            var childSubjects = await _subjectRepository.GetChildSubjects(currentSubject.Id);

            if (childSubjects.Any())
            {
                childSubjects.ForEach(x => x.ClearChildSubjects());
                for (int i = 0; i < childSubjects.Count; i++)
                {
                    Subject subject = childSubjects[i];
                    var childSubjectList = new List<Subject>();
                    var testChildSubjects = await GetAllSubjects(subject, childSubjectList);

                    currentSubject.AddChildSubjectsRange(testChildSubjects);
                }
                subjectListHierarchy.Add(currentSubject);
            }
            else
            {
                subjectListHierarchy.Add(currentSubject);
            }

            return subjectListHierarchy;
        }
    }
}
