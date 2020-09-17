using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;

namespace TP.DataContracts
{
    public interface IUserRoleRepository
    {
        void AddUserRole(UserRole userRole);
        Task<List<UserRole>> GetAll();
        Task<List<UserRole>> GetAllByEmployeeId(Guid employeeId);
        Task<UserRole> GetById(Guid Id);
        void UpdateUserRole(UserRole userRole);
        Task SaveChanges();
        void DeleteUserRole(UserRole userRole);
    }
}
