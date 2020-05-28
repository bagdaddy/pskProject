using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Services.DTOServices.Models;

namespace TP.DataContracts
{
    public interface ITeamControllerService
    {
        Task<List<Employee>> GetAllTeams(Employee currentEmployee, List<Employee> employeeListHierarchy);
    }
}
