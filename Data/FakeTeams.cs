using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.Data
{
    public static class FakeTeams
    {
        public static List<Team> getAll()
        {
            return teams;
        }

        public static Team getById(Guid id)
        {
            return teams.FirstOrDefault(team => team.Id == id);
        }

        private static List<Team> teams = new List<Team>()
        {
            new Team
            {
                Id = Guid.Parse("b09eb9d3-9abc-4dd0-8f7d-4518ff961cb9"),
                teamLeader = FakeEmployees.getById(Guid.Parse("b96f7695-c9d9-4b5c-849e-4219083d6220")),
                teamMembers = new List<Employee>()
                {
                    FakeEmployees.getById(Guid.Parse("63f4e537-cb7b-4fe0-beca-cc080e42552d")),
                    FakeEmployees.getById(Guid.Parse("1c8c9ce0-2251-4ef3-ab0b-cfc7dd7a2949")),
                    FakeEmployees.getById(Guid.Parse("5a677c6e-56e5-4cf1-9c64-157b483e8eff"))
                }
            },
            new Team
            {
                Id = Guid.Parse("15d6b499-bc02-498f-8fea-606950f27594"),
                teamLeader = FakeEmployees.getById(Guid.Parse("5a677c6e-56e5-4cf1-9c64-157b483e8eff")),
                teamMembers = new List<Employee>()
                {
                    FakeEmployees.getById(Guid.Parse("ca595500-ac9b-4d55-a887-7ec28bad3983")),
                    FakeEmployees.getById(Guid.Parse("4cce91af-6898-4b09-9d8d-043bbdc4e654"))
                    
                }
            },
            new Team
            {
                Id = Guid.Parse("88616737-20e1-4cea-845b-e374967d4a89"),
                teamLeader = FakeEmployees.getById(Guid.Parse("4cce91af-6898-4b09-9d8d-043bbdc4e654")),
                teamMembers = new List<Employee>()
                {
                    FakeEmployees.getById(Guid.Parse("c85d09e8-8658-456c-8595-502076661f41")),
                    FakeEmployees.getById(Guid.Parse("e214ea22-5d39-4cea-bf71-3749806e7bdd"))
                }
            },
            new Team
            {
                Id = Guid.Parse("281a0b45-921a-4e2a-ae55-b9e5f821599f"),
                teamLeader = FakeEmployees.getById(Guid.Parse("e214ea22-5d39-4cea-bf71-3749806e7bdd")),
                teamMembers = new List<Employee>()
                {
                }
            }
        };
    }
}
