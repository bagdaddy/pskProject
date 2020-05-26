using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAllTeams();

        Task<Team> GetTeamById(Guid id);

        Task DeleteTeam(Guid id);

        Task<Team> UpdateTeam(Team request, Guid id);
    }
}
