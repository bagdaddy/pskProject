using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Models.ResponseModels;

namespace TP.Models.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Employee, TeamResponseModel>();
            CreateMap<EmployeeSubject, BareSubjectResponseModel>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.Subject.Id))
                .ForMember(dest =>
                    dest.Name,
                    opt => opt.MapFrom(src => src.Subject.Name));
            CreateMap<UserRole, UserRoleResponseModel>();
        }
    }
}
