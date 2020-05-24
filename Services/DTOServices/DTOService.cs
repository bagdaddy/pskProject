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
        public List<EmployeeDTO> employeesToDTO(List<Employee> employeeList)
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

        public EmployeeDTO employeeToDTO(Employee employee)
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

        public List<TeamDTO> teamsToDTO(List<Team> teamList)
        {
            return teamList.Select(team =>
            new TeamDTO()
            {
                Id = team.Id,
                teamLeader = employeeToDTO(team.TeamLeader),
                teamMembers = employeesToDTO(team.TeamMembers)
            }).ToList();
        }

        public TeamDTO teamToDTO(Team team)
        {
            return new TeamDTO()
            {
                Id = team.Id,
                teamLeader = employeeToDTO(team.TeamLeader),
                teamMembers = employeesToDTO(team.TeamMembers)
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
    }
}
