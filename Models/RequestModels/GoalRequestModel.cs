using System;
using TP.Data.Entities;

namespace TP.Models.RequestModels
{
    public class GoalRequestModel
    {
        public Guid EmployeeId { get; set; }
        public Guid SubjectId { get; set; }
    }
}