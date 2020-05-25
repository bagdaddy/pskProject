using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Data
{
    public class TeamRepository : ITeamRepository
    {

        //TODO: Inject appDbContext(Factory)
        public void delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Team> getAll()
        {
            throw new NotImplementedException();
        }

        public Team getById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Team updateTeam(Team request, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
