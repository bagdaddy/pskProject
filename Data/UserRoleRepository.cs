using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TP.Data.Contexts;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Controllers;
using TP.Services;

namespace TP.Data
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;
        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddUserRole(UserRole userRole)
        {
            _context.Add(userRole);
        }

        public Task<List<UserRole>> GetAll()
        {
            var userRoleList = _context.UserRoles
                .Include(x => x.EmployeeList)
                .ToListAsync();
            return userRoleList;
        }

        private List<Guid> getFlatListOfSubordinates(List<Employee> subordinates, List<Guid> flatList)
        {
            foreach(Employee employee in subordinates)
            {
                flatList.Add(employee.Id);
                if(employee.Subordinates.Count > 0)
                {
                    flatList = getFlatListOfSubordinates(employee.Subordinates, flatList);
                }
            }
            return flatList;
        }

        public async Task<List<UserRole>> GetAllByEmployeeId(Guid employeeId)
        {
            var userRoleList = await _context.UserRoles
                .Include(x => x.EmployeeList)
                .ToListAsync();

            var teamRepository = new TeamRepository(_context);
            var tcs = new TeamControllerService(teamRepository);
            var team = await tcs.GetTeam(employeeId);
            List<Guid> subords = getFlatListOfSubordinates(team, new List<Guid>());
            foreach (UserRole userRole in userRoleList){
                var newList = new List<Employee>();
                foreach(Employee employee in userRole.EmployeeList)
                {
                    if(subords.Contains(employee.Id))
                    {
                        newList.Add(employee);
                    }
                }
                userRole.EmployeeList = newList;
            }

            return userRoleList;
        }

        public Task<UserRole> GetById(Guid Id)
        {
            var userRole = _context.UserRoles
                            .Include(x => x.EmployeeList)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == Id);
            return userRole;
        }

        public void UpdateUserRole(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
        }
        public void DeleteUserRole(UserRole userRole)
        {
            _context.Remove(userRole);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
