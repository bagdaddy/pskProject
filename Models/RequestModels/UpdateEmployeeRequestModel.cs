using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.Models.RequestModels
{
    public class UpdateEmployeeRequestModel
    {
        public Guid Id { get; set; }
        [MaxLength(25)]
        public string FirstName { get; set; }
        [MaxLength(25)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public List<Subject> LearnedSubjects { get; set; }
    }
}
