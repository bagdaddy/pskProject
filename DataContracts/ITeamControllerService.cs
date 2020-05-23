using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    interface ITeamControllerService
    {
        List<Team> getAll();

        Team getById(Guid id);

        void delete(Guid id);

        //TODO: Change Employee param to UpdateEmployeeRequest
        Team updateEmployee(Team request, Guid id);
    }
}
