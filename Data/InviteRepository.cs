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
    public class InviteRepository : IInviteRepository
    {
        private readonly AppDbContext _context;
        public InviteRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Invite invite)
        {
            _context.Add(invite);
        }

        public void Delete(Invite invite)
        {
            _context.Remove(invite);
        }

        public Task<Invite> GetById(Guid id)
        {
            return _context.Invites
                    .Include(x => x.Employee)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
