using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Models.ResponseModels
{
    public class GoalsTeamResponseModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BareSubjectResponseModel> LearnedSubjects { get; set; }
        public List<SimplifiedGoalResponseModel> Goals { get; set; }
        public List<GoalsTeamResponseModel> Subordinates { get; set; }
    }
}
