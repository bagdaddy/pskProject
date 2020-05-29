using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Models.ResponseModels;

namespace TP.Models.Profiles
{
    public class InviteProfile : Profile
    {
        public InviteProfile()
        {
            CreateMap<Invite, InviteResponseModel>();
            CreateMap<Employee, EmailEmployeeResponseModel>();
        }
    }
}
