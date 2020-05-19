using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    interface IEmployeesRepository
    {
        List<Employee> GetAll();

        Employee getById(Guid id);

        void Delete(Guid id);

        //TODO: Change Employee param to UpdateEmployeeRequest
        Employee UpdateEmployee(Employee request, Guid id);
    }
}
