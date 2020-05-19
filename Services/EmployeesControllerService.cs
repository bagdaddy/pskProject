using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Services
{
    public class EmployeesControllerService : IEmployeesControllerService
    {
        private static readonly IEmployeesRepository employeesRepository;
        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> getById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateEmployee(Employee request, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
