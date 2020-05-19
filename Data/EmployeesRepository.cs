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

        List<Employee> IEmployeesRepository.GetAll()
        {
            return FakeEmployees.getAll();
        }
        void IEmployeesRepository.Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        Employee IEmployeesRepository.UpdateEmployee(Employee request, Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
