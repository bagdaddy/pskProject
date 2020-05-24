using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    public interface IEmployeesRepository
    {
        List<Employee> getAll();

        Employee getById(Guid id);

        void delete(Employee employee);

        //TODO: Change Employee param to UpdateEmployeeRequest
        void updateEmployee(Employee employee);

    }
}
