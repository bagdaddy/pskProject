using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.Models.RequestModels
{
    public class SubjectRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SubjectModel ParentSubject { get; set; }
    }
}
