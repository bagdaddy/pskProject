using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.Models.RequestModels
{
    public class SubjectRequestModel
    {   
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public Guid? ParentSubjectId { get; set; }
    }
}
