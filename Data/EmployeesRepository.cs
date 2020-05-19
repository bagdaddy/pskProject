using System;
using System.Collections.Generic;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Data
{
    public class EmployeesRepository : IEmployeesRepository
    {

        //TODO: Inject appDbContext(Factory)

        Employee IEmployeesRepository.getById(Guid id)
        {
            return FakeEmployees.getById(id);
        }

        List<Employee> IEmployeesRepository.getAll()
        {
            return FakeEmployees.getAll();
        }
        void IEmployeesRepository.delete(Guid id)
        {
            throw new NotImplementedException();
        }

        Employee IEmployeesRepository.updateEmployee(Employee request, Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
