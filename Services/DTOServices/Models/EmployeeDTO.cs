using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.Services.DTOServices.Models
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public Guid? BossId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<SubjectWithoutParentDTO> Subjects { get; set; }
        public Guid? UserRoleId { get; set; }
    }
}
