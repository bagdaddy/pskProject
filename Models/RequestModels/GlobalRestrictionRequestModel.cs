using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Models.RequestModels
{
    public class GlobalRestrictionRequestModel
    {
        [Required]
        public int? GlobalDayLimit { get; set; }
    }
}
