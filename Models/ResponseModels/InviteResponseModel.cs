using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Models.ResponseModels
{
    public class InviteResponseModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public EmailEmployeeResponseModel Employee { get; set; }
    }
}
