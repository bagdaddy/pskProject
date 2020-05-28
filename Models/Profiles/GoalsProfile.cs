using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Models.ResponseModels;

namespace TP.Models.Profiles
{
    public class GoalsProfile : Profile
    {
        public GoalsProfile()
        {
            CreateMap<Subject, BareSubjectResponseModel>();
            CreateMap<Employee, EmployeeResponseModel>();
            CreateMap<Goal, GoalsResponseModel>();
        }
    }
}
