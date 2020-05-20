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
                Name = "Mes heyhey Inzinieriai",
                teamLeader = FakeEmployees.getAll()[0],
                teamMembers = FakeEmployees.getAll().GetRange(1, 3)
            },
            new Team
            {
                Id = Guid.Parse("15d6b499-bc02-498f-8fea-606950f27594"),
                Name = "Mes woohoo dizaineriuxeriai",
                teamLeader = FakeEmployees.getAll()[4],
                teamMembers = FakeEmployees.getAll().GetRange(5, 8)
            }
        };
    }
}
