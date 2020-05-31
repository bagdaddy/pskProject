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
        public List<EmployeeDTO> EmployeesToDTO(List<Employee> employeeList);

        public EmployeeDTO EmployeeToDTO(Employee employee);

        public SubjectDTO SubjectToDTO(Subject subject);

        public List<SubjectDTO> SubjectsToDTO(List<Subject> subjectList);
    }
}
