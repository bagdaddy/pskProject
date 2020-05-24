using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Contexts;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Models.RequestModels;

namespace TP.Data
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly AppDbContext _context;
        public EmployeesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<Employee> GetById(Guid id)
        {
            using (var appDbContext = _context)
            {
                return await GetEmployee(appDbContext, id);
            }
        }

        public async Task<List<Employee>> GetAll()
        {
            using (var appDbContext = _context)
            {
                return await _context.Employees
                    .AsNoTracking()
                    .ToListAsync();
            }
        }

        public async Task Delete(Guid employeeId)
        {
            using (var appDbContext = _context)
            {
                Employee employeeToDelete = await GetEmployee(appDbContext, employeeId);
                appDbContext.Employees.Remove(employeeToDelete);
                await appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Employee> UpdateEmployee(UpdateEmployeeRequestModel request)
        {
            using (var appDbContext = _context)
            {
                Employee employee = await GetEmployee(appDbContext, request.Id);
                appDbContext.Entry(employee).CurrentValues.SetValues(
                    new
                    {
                        FirstName = request.FirstName ?? employee.FirstName,
                        LastName = request.LastName ?? employee.LastName,
                        Email = request.Email ?? employee.Email
                    });
                if(request.LearnedSubjects != null)
                {
                    UpdateEmployeeSubjects(appDbContext, request, employee);
                }

                await appDbContext.SaveChangesAsync();
                return employee;
            }
        }

        public async Task CreateEmployee(Employee employee)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
        }

        private async Task<Employee> GetEmployee(AppDbContext context, Guid id)
        {
            Employee employee = await context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }
            return employee;
        }

        private void UpdateEmployeeSubjects(AppDbContext context, UpdateEmployeeRequestModel request, Employee employee)
        {
            List<EmployeeSubject> employeeSubjectList = new List<EmployeeSubject>();
            foreach (Subject subject in request.LearnedSubjects)
            {

                EmployeeSubject employeeSubject = new EmployeeSubject()
                {
                    Employee = employee,
                    EmployeeId = employee.Id,
                    Subject = subject,
                    SubjectId = subject.Id
                };
                employeeSubjectList.Add(employeeSubject);
            }
            employee.LearnedSubjects = employeeSubjectList;
        }
    }
}
