using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Models.RequestModels;

namespace TP.DataContracts
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(Guid id);
        Task CreateEmployee(Employee employee);
        Task Delete(Guid employeeId);
        Task<Employee> UpdateEmployee(Guid id, UpdateEmployeeRequestModel request);

    }
}
