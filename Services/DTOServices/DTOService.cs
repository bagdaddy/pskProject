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
            return new EmployeeDTO()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email
            };
        }

        public List<TeamDTO> teamsToDTO(List<Team> teamList)
        {
            return teamList.Select(team =>
            new TeamDTO()
            {
                Id = team.Id,
                teamLeader = employeeToDTO(team.teamLeader),
                teamMembers = employeesToDTO(team.teamMembers)
            }).ToList();
        }

        public TeamDTO teamToDTO(Team team)
        {
            return new TeamDTO()
            {
                Id = team.Id,
                teamLeader = employeeToDTO(team.teamLeader),
                teamMembers = employeesToDTO(team.teamMembers)
            };
        }
    }
}
