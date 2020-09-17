using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.Models.ResponseModels
{
    public class UserRoleResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Employee> EmployeeList { get; set; }

        public UserRoleResponseModel(Guid Id, string Name, List<Employee> EmployeeList)
        {
            this.Id = Id;
            this.Name = Name;
            this.EmployeeList = EmployeeList;
        }

        public static List<UserRoleResponseModel> ToResponseModelList(List<UserRole> userRoles)
        {
            return userRoles.Select(ToResponseModel).ToList();
        }
        public static UserRoleResponseModel ToResponseModel(UserRole userRole)
        {
            return new UserRoleResponseModel(userRole.Id, userRole.Name, userRole.EmployeeList);
        }
    }
}
