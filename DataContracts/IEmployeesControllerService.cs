using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    interface IEmployeesControllerService
    {
        Task<List<Employee>> GetAll();

        Task<Employee> getById(Guid id);

        Task Delete(Guid id);

        //TODO: Change Employee param to UpdateEmployeeRequest
        Task<Employee> UpdateEmployee(Employee request, Guid id);
    }
}
