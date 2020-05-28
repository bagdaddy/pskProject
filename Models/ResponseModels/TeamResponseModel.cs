using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.Models.ResponseModels
{
    public class TeamResponseModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BareSubjectResponseModel> LearnedSubjects { get; set; }
        public List<TeamResponseModel> Subordinates { get; set; }
    }
}
