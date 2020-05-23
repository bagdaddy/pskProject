using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.Services.DTOServices.Models
{
    public class SubjectDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject ParentSubject { get; set; }
        public string Description { get; set; }
        
    }
}
