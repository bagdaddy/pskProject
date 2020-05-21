using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Services
{
    public class TeamControllerService : ITeamControllerService
    {

        private readonly ITeamRepository teamRepository = new TeamRepository();
        public void delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Team> getAll()
        {
            return teamRepository.getAll();
        }

        public Team getById(Guid id)
        {
            return teamRepository.getById(id);
        }

        public Team updateEmployee(Team request, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
