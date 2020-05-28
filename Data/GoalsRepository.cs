using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TP.Data.Contexts;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Data
{
    public class GoalsRepository : IGoalsRepository
    {
        private readonly AppDbContext _context;
        
        public async Task AddGoal(Goal goal)
        {
            _context.Add(goal);
            await _context.SaveChangesAsync();
        }
        //get signle by object id
        public async Task<Goal> GetById(Guid id)
        {
            return await _context.Goals
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
        }
        //get all by workder id
        public async Task<List<Goal>> GetAll(Guid employeeId)
        {
            var goalList = _context.Goals.Where(x => x.Employee.Id == employeeId)
                .AsNoTracking()
                    .ToListAsync();
            return await goalList;
        }

        public async Task Delete(Guid id)
        {
            using (var appDbContext = _context)
            {
                Goal goalToDelete = await GetById(id);
                appDbContext.Goals.Remove(goalToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}