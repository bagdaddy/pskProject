using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TP.Data.Contexts;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Data
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly EmployeeContext _context;
        public EmployeesRepository(EmployeeContext context)
        {
            _context = context;
        }

        public Employee getById(Guid id)
        {
            return _context.Employees
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Employee> getAll()
        {
            var employeeList = _context.Employees
                .AsNoTracking()
                .ToList();
            return employeeList;
        }
        public void delete(Employee employee)
        {
            _context.Remove(employee);
        }

        public void updateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
        }
    }
}
