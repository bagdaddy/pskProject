using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Models.RequestModels
{
    public class LocalRestrictionRequestModel
    {
        [Required]
        public Guid EmployeeId { get; set; }
        [Required]
        public int? LocalDayLimit { get; set; }
    }
}
