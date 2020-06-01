using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Services.DTOServices.Models;

namespace TP.Services
{
    public class DTOService : IDTOService
    {
        public List<EmployeeDTO> EmployeesToDTO(List<Employee> employeeList)
        {
            return employeeList.Select(employee =>
            new EmployeeDTO()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Subjects = GetSubjectListWithoutParent(employee.LearnedSubjects)
            }).ToList();
        }

        public EmployeeDTO EmployeeToDTO(Employee employee)
        {
            return new EmployeeDTO()
            {
                Id = employee.Id,
                BossId = employee.BossId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Subjects = GetSubjectListWithoutParent(employee.LearnedSubjects)
            };
        }

        public List<SubjectDTO> SubjectsToDTO(List<Subject> subjectList)
        {
            return subjectList.Select(subject =>
            new SubjectDTO
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description
            }).ToList();
        }

        public SubjectDTO SubjectToDTO(Subject subject)
        {
            List<EmployeeWithoutSubjectsDTO> employeeList = new List<EmployeeWithoutSubjectsDTO>();
            if (subject.EmployeesWhoLearnedIt != null)
            {
                foreach (var employee in subject.EmployeesWhoLearnedIt)
                {
                    employeeList.Add(GetEmployeeDTOWithoutSubjects(employee.Employee));
                }
            }
            return new SubjectDTO
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description,
                Employees = employeeList
            };
        }

        private List<SubjectWithoutParentDTO> GetSubjectListWithoutParent(List<EmployeeSubject> subjectList)
        {
            List<SubjectWithoutParentDTO> subjects = new List<SubjectWithoutParentDTO>();
            if (subjectList != null)
            {
                foreach (var subject in subjectList)
                {
                    subjects.Add(SubjectToDTOWithoutParent(subject.Subject));
                }
            }
            return subjects;
        }

        private SubjectWithoutParentDTO SubjectToDTOWithoutParent(Subject subject)
        {
            return new SubjectWithoutParentDTO()
            {
                Name = subject.Name,
                Id = subject.Id,
                Description = subject.Description
            };
        }

        private List<EmployeeWithoutSubjectsDTO> GetEmployeeListWithoutSubjects(List<EmployeeSubject> employeeList)
        {
            List<EmployeeWithoutSubjectsDTO> employees = new List<EmployeeWithoutSubjectsDTO>();
            if (employeeList != null)
            {
                foreach (var employee in employeeList)
                {
                    employees.Add(GetEmployeeDTOWithoutSubjects(employee.Employee));
                }
            }
            return employees;
        }
        private EmployeeWithoutSubjectsDTO GetEmployeeDTOWithoutSubjects(Employee employee)
        {
            return new EmployeeWithoutSubjectsDTO
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Id = employee.Id
            };
        }
    }
}
