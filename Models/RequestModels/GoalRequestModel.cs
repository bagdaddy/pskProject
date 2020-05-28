using System;
using TP.Data.Entities;

namespace TP.Models.RequestModels
{
    public class GoalRequestModel
    {
        public Employee Employee { get; set; }
        public Subject Subject { get; set; }
    }
}