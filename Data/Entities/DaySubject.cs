using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Data.Entities
{
    public class DaySubject
    {
        public Guid DayId { get; set; }
        public Day Day { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
