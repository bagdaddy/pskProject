using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    interface ITeamRepository
    {
        List<Team> getAll();

        Team getById(Guid id);

        void delete(Guid id);

        Team updateTeam(Team request, Guid id);
    }
}
