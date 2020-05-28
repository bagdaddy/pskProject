using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TP.Data.Contexts;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Data
{
    public class GoalsRepository : IGoalsRepository
    {
        private readonly AppDbContext _context;
        
        public GoalsRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddGoal(Goal goal)
        {
            _context.Add(goal);
        }
        //get signle by object id
        public Task<List<Goal>> GetByIdWithMembers(Guid id)
        {
            return _context.Goals
                        .Include(x => x.Employee)
                        .Include(x => x.Subject)
                        .AsNoTracking()
                        .Where(x => x.EmployeeId == id).ToListAsync();
        }
        public Task<Goal> GetById(Guid id)
        {
            return _context.Goals
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == id);
        }
        //get all by workder id
        public Task<List<Goal>> GetAll(Guid employeeId)
        {
            var goalList = _context.Goals.Where(x => x.Employee.Id == employeeId)
                            .AsNoTracking()
                            .ToListAsync();
            return goalList;
        }

        public void Remove(Goal goal)
        {
            _context.Remove(goal);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}