using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TP.Data.Entities;

namespace TP.DataContracts
{
    public interface IDaysRepository
    {
        Task<List<Day>> GetAll(Guid employeeid);
        Task<Day> GetSingle(Guid id);
        Task<Day> GetSingle(Guid employeeid, DateTime date);
        Task<int> GetThisQuarter(Guid employeeid, int quarter);
        void Delete(Guid id);
    }
}
