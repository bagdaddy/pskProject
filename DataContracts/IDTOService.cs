using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Services.DTOServices.Models;

namespace TP.DataContracts
{
    public interface IDTOService
    {
        public List<EmployeeDTO> employeesToDTO(List<Employee> employeeList);

        public EmployeeDTO employeeToDTO(Employee employee);

        public List<TeamDTO> teamsToDTO(List<Team> teamList);

        public TeamDTO teamToDTO(Team team);
    }
}
