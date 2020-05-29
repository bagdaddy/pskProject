using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Services
{
    public class GoalsControllerService
    {
        private readonly IGoalsRepository _repository;
        private readonly ITeamRepository _teamRepository;
        public GoalsControllerService(IGoalsRepository repository, ITeamRepository teamRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
        }
        public async Task DeleteGoal(Guid id)
        {
            try
            {
                var goal = await _repository.GetById(id);

                _repository.Remove(goal);
                await _repository.SaveChanges();
            }
            catch (DBConcurrencyException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
