using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Models.RequestModels
{
    public class UpdateTeamRequestModel
    {
        public Guid Id { get; set; }
        public Guid BossId { get; set; }
    }
}
