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
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;
        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Employee>> GetAll()
        {
            var teamList = _context.Users
                            .Include(x => x.LearnedSubjects)
                            .ThenInclude(xx => xx.Subject)
                            .AsNoTracking()
                            .ToListAsync();

            return teamList;
        }

        public Task<Employee> GetById(Guid id)
        {
            return _context.Users
                    .Include(x => x.LearnedSubjects)
                    .ThenInclude(xx => xx.Subject)
                    .Include(x => x.Goals)
                    .ThenInclude(xx => xx.Subject)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Employee> GetByIdWithSubordinates(Guid id)
        {
            return _context.Users
                    .Include(x => x.Subordinates)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateTeam(UpdateTeamRequestModel requestModel)
        {
            Employee employee = await GetById(requestModel.Id);
            employee.BossId = requestModel.BossId;

            _context.Update(employee);
            await _context.SaveChangesAsync();
        }
        public Task<List<Employee>> GetSubordinates(Guid id)
        {
            var subordinateList = _context.Users
                .Include(x => x.LearnedSubjects)
                .ThenInclude(xx => xx.Subject)
                .Include(x => x.Goals)
                .ThenInclude(xx => xx.Subject)
                .Include(x => x.Subordinates)
                .Where(x => x.BossId.HasValue && x.BossId.Value == id)
                .AsNoTracking()
                .ToListAsync();
            return subordinateList;
        }
        public void Delete(Employee employee)
        {
            _context.Remove(employee);
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
