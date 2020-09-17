using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Models.ResponseModels;

namespace TP.Mappings.EntityToResponse
{
    public static class UserRoleMapping
    {
        /* public static List<UserRoleResponseModel> MapToResponse(this List<UserRole> userRoles)
         {
             var responseModel = userRoles.Select(x => new UserRoleResponseModel
             {
                 Id = x.Id,
                 Name = x.Name,
                 EmployeeList = x.EmployeeList
             }).ToList();

             return responseModel;
         }*/
    }
}
