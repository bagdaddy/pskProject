using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TP.DataContracts;

namespace TP.Services
{
    public class GoalsControllerService
    {
        private readonly IGoalsRepository _repository;
        public GoalsControllerService(IGoalsRepository repository)
        {
            _repository = repository;
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
