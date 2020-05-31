using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Models.RequestModels
{
    public class DayRequestModel
    {
        public Guid EmployeeId { get; set; }
        public List<Guid> SubjectList { get; set; }
        public DateTime? Date { get; set; }
        public string Comment { get; set; }
    }
}
