using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Models.RequestModels
{
    public class SubjectModel
    {
        public string Name { get; set; }
        public SubjectModel ParentSubject { get; set; }
        public string Description { get; set; }
    }
}
