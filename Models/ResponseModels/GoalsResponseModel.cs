using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Models.ResponseModels
{
    public class GoalsResponseModel
    {
        public Guid Id { get; set; }
        public EmployeeResponseModel Employee {get; set;}
        public BareSubjectResponseModel Subject { get; set; }
    }
}
