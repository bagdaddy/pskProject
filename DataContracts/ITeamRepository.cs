using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Models.RequestModels;

namespace TP.DataContracts
{
    public interface ITeamRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(Guid id);
        void Delete(Employee employee);
        Task SaveChanges();
        Task UpdateTeam(UpdateTeamRequestModel request);
        Task<List<Employee>> GetSubordinates(Guid id);
    }
}
