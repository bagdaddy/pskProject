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
            return new EmployeeDTO()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email
            };
        }
    }
}
