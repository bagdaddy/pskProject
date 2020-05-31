using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TP.Data.Contexts;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Extensions;
using TP.Models.RequestModels;

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
                .Where(x => x.Employee.Id == employeeid)
                .Include(x => x.DaySubjectList)
                .ThenInclude(ds => ds.Subject)
                .AsNoTracking()
                .ToListAsync();
            return await dayList;
        }
        //get signle by object id
        public async Task<Day> GetSingle(Guid id)
        {
            return await _context.Days
                .Include(x => x.DaySubjectList)
                .ThenInclude(ds => ds.Subject)
                .Include(x => x.Employee)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
        }
        //get signle by worker id and date
        public async Task<Day> GetSingle(Guid employeeid, DateTime date)
        {
            return await _context.Days
                    .AsNoTracking()
                    .Include(x => x.Employee)
                    .FirstOrDefaultAsync(x => x.Employee.Id == employeeid && x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day);
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
            var workerDaysThisQuarter = await _context.Days
                .Where(x => x.Employee.Id == employeeid)
                .ToListAsync();
            var count = 0;
            foreach (var day in workerDaysThisQuarter)
            {
                if(day.Date.GetQuarter() == quarter)
                {
                    count += 1;
                }
            }
            return count;
        }
        // isveda darbuotoju id sarasa pagal subject id
        public async Task<List<Employee>> GetEmployeesBySubject(Guid subjectId)
        {
            var employeesList = _context.Days
                .Include(x => x.Employee)
                .Include(x => x.DaySubjectList)
                .Where(x => x.DaySubjectList.Any(y => y.SubjectId == subjectId)).Select(x => x.Employee)
                .ToListAsync();
            return await employeesList;
        }

        public async Task Create(DayRequestModel model)
        {
            Day dayToAdd = new Day
            {
                Date = model.Date ?? DateTime.Now,
                EmployeeId = model.EmployeeId,
                Id = Guid.NewGuid(),
                Comment = model.Comment
            };
            var daySubjectList = new List<DaySubject>();
            foreach (var subject in model.SubjectList)
            {
                daySubjectList.Add(new DaySubject
                {
                    DayId = dayToAdd.Id,
                    SubjectId = subject
                });
            }
            await _context.Days.AddAsync(dayToAdd);
            await _context.DaySubjects.AddRangeAsync(daySubjectList);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Guid id, DayRequestModel model)
        {
            
                Day day = await _context.Days
                .Include(x => x.DaySubjectList)
                //.ThenInclude(ds => ds.Subject)
                .FirstOrDefaultAsync(x => x.Id == id);
                _context.Entry(day).CurrentValues.SetValues(
                    new
                    {
                        Date = model.Date ?? day.Date,
                        model.EmployeeId,
                        Id = id,
                        Comment = model.Comment ?? day.Comment
                    });
                UpdateDaySubjects(_context, model, day);

                await _context.SaveChangesAsync();
            
        }

        private void UpdateDaySubjects(AppDbContext context, DayRequestModel model, Day day)
        {
            List<DaySubject> daySubjectList = new List<DaySubject>();
            if (model.SubjectList != null)
            {
                foreach (Guid subject in model.SubjectList)
                {

                    daySubjectList.Add(new DaySubject
                    {
                        DayId = day.Id,
                        SubjectId = subject
                    });
                }
            }
            day.DaySubjectList = daySubjectList;
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
