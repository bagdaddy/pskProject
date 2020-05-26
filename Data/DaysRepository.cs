using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TP.Data.Contexts;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Data
{
    public class DaysRepository : IDaysRepository
    {
        private readonly AppDbContext _context;
        public DaysRepository(AppDbContext context)
        {
            _context = context;
        }
        //get all by workder id
        public async Task<List<Day>> GetAll(Guid employeeid)
        {
            var dayList = _context.Days
                .Where(x => x.EmployeesId == employeeid)
                .AsNoTracking()
                .ToListAsync();
            return await dayList;
        }
        //get signle by object id
        public async Task<Day> GetSingle(Guid id)
        {
            return await _context.Days
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
        }
        //get signle by worker id and date
        public async Task<Day> GetSingle(Guid employeeid, DateTime date)
        {
            return await _context.Days
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.EmployeesId == employeeid && x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);
        }
        private int GetQuarter(DateTime fromDate)
        {
            int month = fromDate.Month - 1;
            int month2 = Math.Abs(month / 3) + 1;
            return month2;
        }
        //count days by worker id and quarter
        public async Task<int> GetThisQuarter(Guid employeeid, int quarter)
        {
            var workerDaysThisQuarter = _context.Days
                .CountAsync(x => x.EmployeesId == employeeid && GetQuarter(x.Date) == quarter);
            return await workerDaysThisQuarter;
        }
        //PRIDETI i IDaysRepository // isveda darbuotoju id sarasa pagal subject id
        public async Task<List<Guid>> GetEmployeesBySubject(Guid subjectId)
        {
            var employeesList = _context.Days
                .Where(x => x.SubjectList.Any(y => y.Id == subjectId)).Select(x => x.EmployeesId)
                .ToListAsync();
            return await employeesList;
        }

        public async Task Delete(Guid id)
        {
            using (var appDbContext = _context)
            {
                Day dayToDelete = await GetSingle(id);
                appDbContext.Days.Remove(dayToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
