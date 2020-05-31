using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.Models.RequestModels;

namespace TP.DataContracts
{
    public interface IDaysRepository
    {
        Task<List<Day>> GetAll(Guid employeeid);
        Task<Day> GetSingle(Guid id);
        Task<Day> GetSingle(Guid employeeid, DateTime date);
        Task<int> GetThisQuarter(Guid employeeid, int quarter);
        Task<List<Employee>> GetEmployeesBySubject(Guid subjectId);
        Task Create(DayRequestModel model);
        Task Update(Guid id, DayRequestModel model);
        Task Delete(Guid id);
    }
}
