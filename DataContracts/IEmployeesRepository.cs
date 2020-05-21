using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    interface IEmployeesRepository
    {
        List<Employee> getAll();

        Employee getById(Guid id);

        void delete(Guid id);

        //TODO: Change Employee param to UpdateEmployeeRequest
        Employee updateEmployee(Employee request, Guid id);
    }
}
