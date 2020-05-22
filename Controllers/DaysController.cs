using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;
using TP.DataContracts;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {
        private List<Subject> subjectList;
        private List<Day> list;
        public DaysController()
        {
            subjectList = new List<Subject> {
                new Subject(new Guid("819df009-1d4d-4a57-ac73-55e79973992a"),"dotnet something", null, "this is a topic about something"),
                new Subject(new Guid("ba099e58-9194-4841-b047-5d0c4541bba5"),"java something", null, "this is a topic about something else"),
                new Subject(new Guid("5c478ca2-b93f-4535-adc2-14676398d93e"),"out of ideas", null, "this is a topic about something"),
             };
            list = new List<Day> {
                new Day(new Guid("b96f7695-c9d9-4b5c-849e-4219083d6220"),DateTime.Now,subjectList),
                new Day(new Guid("b96f7695-c9d9-4b5c-849e-4219083d6220"),DateTime.Now.AddDays(-1),subjectList),
                new Day(new Guid("1c8c9ce0-2251-4ef3-ab0b-cfc7dd7a2949"),DateTime.Now.AddDays(-2),subjectList),
            };
        }

        //get all by workder id
        [HttpGet("{workerId}")]
        public async Task<IActionResult> getAll(Guid employeeid)
        {
            var workerDays = list.Where(x => x.EmployeesId == employeeid).ToList();
            if (workerDays.Count != 0) { return Ok(workerDays); }
            return BadRequest("oopsie");
        }

        //get signle by object id
        [HttpGet("{id}")]
        public async Task<IActionResult> getSingle(Guid id)
        {
            var dayId = list.Select(x => x.Id).ToList();
            if (dayId.Contains(id)) { return Ok(list.First(x => x.Id == id)); }
            return BadRequest("oopsie");
        }

        //get signle by worker id and date
        [HttpGet("{workerId}/{date}")]
        public async Task<IActionResult> getSingle(Guid employeeid, DateTime date)
        {
            var workerId = list.Select(x => x.EmployeesId).ToList();
            if (workerId.Contains(employeeid)) { return Ok(list.First(x => x.EmployeesId == employeeid && x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day)); }
            return BadRequest("oopsie");

        }

        //get by worker id and quarter
        [HttpGet("{workerId}/{quarter}")]
        public async Task<IActionResult> getDatesThisQuarter(Guid workerId, int quarter)
        {

        }

        // DELETE by object id
        [HttpDelete("{id}")]
        public void deleteDate(Guid id)
        {
            var itemToRemove = list.SingleOrDefault(x => x.Id == id);
            if (itemToRemove != null)
                list.Remove(itemToRemove);
        }
    }
}

