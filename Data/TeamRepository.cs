using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Contexts;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Data
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;
        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        //TODO: Inject appDbContext(Factory)

        public async Task<List<Team>> GetAllTeams()
        {
            throw new NotImplementedException();
        }

        public async Task<Team> GetTeamById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteTeam(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Team> UpdateTeam(Team request, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
