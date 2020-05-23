using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Models.RequestModels;

namespace TP.Controllers
{

    public class SubjectController: ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        [HttpGet("api/GetSubjects")]
        public IActionResult GetSubjects()
        {
            try
            {
                var subjects = _subjectRepository.GetAll();
                var subjectListHierarchy = new List<Subject>();

                foreach (var subject in subjects)
                {
                    if (!subject.ParentSubjectId.HasValue)
                    {
                        GetAllSubjects(subject, subjectListHierarchy);
                    }
                }
                return Ok(subjectListHierarchy);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("api/GetSubjects/{id}")]
        public IActionResult GetSubjectsById(Guid id)
        {
            try
            {
                var subject = _subjectRepository.GetById(id);
                var list = new List<Subject>();

                GetAllSubjects(subject, list);

                return Ok(subject);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("api/GetSubject/{id}")]
        public IActionResult GetSubjectById(Guid id)
        {
            try
            {
                var subject = _subjectRepository.GetById(id);

                return Ok(subject);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("api/CreateSubject")]
        public IActionResult CreateSubject([FromBody]SubjectRequestModel subjectRequestModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (subjectRequestModel.ParentSubjectId.HasValue)
                {
                    var parentSubject = _subjectRepository.GetById(subjectRequestModel.ParentSubjectId.Value);
                    if (parentSubject == null)
                    {
                        return BadRequest("Parent not found");
                    }
                    var subject = new Subject(subjectRequestModel.Name, subjectRequestModel.ParentSubjectId.Value , subjectRequestModel.Description);

                    parentSubject.AddChildSubjects(subject);
                    _subjectRepository.AddSubject(subject);
                    _subjectRepository.UpdateSubjects(parentSubject);
                }
                else
                {
                    var subject = new Subject(subjectRequestModel.Name, null, subjectRequestModel.Description);
                    _subjectRepository.AddSubject(subject);
                }
                _subjectRepository.SaveChanges();
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("api/UpdateSubject")]
        public IActionResult UpdateSubject([FromBody]UpdateSubjectRequestModel updateSubjectRequestModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var subject = _subjectRepository.GetById(updateSubjectRequestModel.Id);

                if (subject == null)
                {
                    return BadRequest("Subject not found");
                }

                subject.UpdateSubject(updateSubjectRequestModel.Name, updateSubjectRequestModel.Description);
                _subjectRepository.UpdateSubjects(subject);
                _subjectRepository.SaveChanges();

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("api/DeleteSubject/{id}")]
        public IActionResult DeleteSubject(Guid id)
        {
            try
            {
                var subject = _subjectRepository.GetByIdWithChild(id);

                if(subject == null)
                {
                    return BadRequest("Subject not found");
                }
                if(subject.ChildSubjects.Any())
                {
                    return BadRequest("Cannot remove subject that still has subjects attached to it");
                }
                if(subject.ParentSubjectId.HasValue)
                {
                    var parentSubject = _subjectRepository.GetById(subject.ParentSubjectId.Value);
                    parentSubject.DeleteChildSubjects(subject);
                }

                _subjectRepository.Delete(subject);
                _subjectRepository.SaveChanges();

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        private List<Subject> GetAllSubjects(Subject currentSubject, List<Subject> subjectListHierarchy)
        {
            var childSubjects = _subjectRepository.GetChildSubjects(currentSubject.Id);

            if (childSubjects.Any())
            {
                childSubjects.ForEach(x => x.ClearChildSubjects());
                foreach (var subject in childSubjects)
                {
                    var childSubjectList = new List<Subject>();
                    var testChildSubjects = GetAllSubjects(subject, childSubjectList);

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
