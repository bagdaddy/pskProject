using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    public interface IGoalsRepository
    {
        Task<List<Goal>> GetAll(Guid employeeId);
        Task<Goal> GetById(Guid id);
        Task AddGoal(Goal goal);
        Task Delete(Guid id);
    }
}