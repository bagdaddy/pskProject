using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    public interface IRestrictionRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(Guid id);
        Task<Employee> GetAny();
        void Update(Employee employee);
        void Delete();
        Task SaveChanges();
    }
}
