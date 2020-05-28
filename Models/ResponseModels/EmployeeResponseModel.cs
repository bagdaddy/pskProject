using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Models.ResponseModels
{
    public class EmployeeResponseModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
