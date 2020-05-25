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
                Email = employee.Email
            }).ToList();
        }

        public EmployeeDTO EmployeeToDTO(Employee employee)
        {
            List<SubjectWithoutParentDTO> subjectList = new List<SubjectWithoutParentDTO>();
            if (employee.LearnedSubjects != null)
            {
                foreach (var subject in employee.LearnedSubjects)
                {
                    subjectList.Add(subjectToDTOWithoutParent(subject.Subject));
                }
            }
            
            return new EmployeeDTO()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Subjects = subjectList
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
                    employeeList.Add(employeeToDTOWithoutSubjects(employee.Employee));
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

        public List<TeamDTO> TeamsToDTO(List<Team> teamList)
        {
            return teamList.Select(team =>
            new TeamDTO()
            {
                Id = team.Id,
                teamLeader = EmployeeToDTO(team.TeamLeader),
                teamMembers = EmployeesToDTO(team.TeamMembers)
            }).ToList();
        }

        public TeamDTO TeamToDTO(Team team)
        {
            return new TeamDTO()
            {
                Id = team.Id,
                teamLeader = EmployeeToDTO(team.TeamLeader),
                teamMembers = EmployeesToDTO(team.TeamMembers)
            };
        }

        private SubjectWithoutParentDTO subjectToDTOWithoutParent(Subject subject)
        {
            return new SubjectWithoutParentDTO()
            {
                Name = subject.Name,
                Id = subject.Id,
                Description = subject.Description
            };
        }

        private EmployeeWithoutSubjectsDTO employeeToDTOWithoutSubjects(Employee employee)
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
