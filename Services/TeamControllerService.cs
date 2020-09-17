using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Services
{
    public class TeamControllerService : ITeamControllerService
    {
        private readonly ITeamRepository _teamRepository;
        public TeamControllerService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public async Task<List<Employee>> GetAllTeams(Employee currentEmployee, List<Employee> employeeListHierarchy)
        {
            var subordinates = await _teamRepository.GetSubordinates(currentEmployee.Id);

            if (subordinates.Any())
            {
                subordinates.ForEach(x => x.Subordinates.Clear());
                for (int i = 0; i < subordinates.Count; i++)
                {
                    Employee employee = subordinates[i];
                    var subordinateList = new List<Employee>();
                    var testSubordinates = await GetAllTeams(employee, subordinateList);

                    currentEmployee.Subordinates.AddRange(testSubordinates);
                }

                employeeListHierarchy.Add(currentEmployee);
            }
            else
            {
                employeeListHierarchy.Add(currentEmployee);
            }

            return employeeListHierarchy;
        }

        public async Task<List<Employee>> GetTeam(Guid employeeId)
        {
            try
            {
                var team = await _teamRepository.GetById(employeeId);
                var list = new List<Employee>();

                return await this.GetAllTeams(team, list);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
