using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    public interface IGoalsRepository
    {
        Task<List<Goal>> GetAll(Guid employeeId);
        Task<List<Goal>> GetByIdWithMembers(Guid id);
        Task<Goal> GetById(Guid id);
        void AddGoal(Goal goal);
        void Remove(Goal goal);
        Task SaveChanges();
    }
}