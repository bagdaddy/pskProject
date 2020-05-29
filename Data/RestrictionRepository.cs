using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Contexts;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Data
{
    public class RestrictionRepository : IRestrictionRepository
    {
        private readonly AppDbContext _context;
        public RestrictionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAll()
        {
            var list = _context.Users
                        .AsNoTracking()
                        .ToListAsync();
            return list;
        }

        public Task<Employee> GetById(Guid id)
        {
            var employee = _context.Users
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Id == id);
            return employee;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Employee employee)
        {
            _context.Update(employee);
        }
    }
}
