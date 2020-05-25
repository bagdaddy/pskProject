using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController
    {
        private List<Goal> list;
        public GoalsController()
        {
            list = new List<Goal> {
                new Goal(new Guid("b96f7695-c9d9-4b5c-849e-4219083d6220"),new Guid("819df009-1d4d-4a57-ac73-55e79973992a")),
                new Goal(new Guid("b96f7695-c9d9-4b5c-849e-4219083d6220"),new Guid("ba099e58-9194-4841-b047-5d0c4541bba5")),
                new Goal(new Guid("1c8c9ce0-2251-4ef3-ab0b-cfc7dd7a2949"),new Guid("5c478ca2-b93f-4535-adc2-14676398d93e")),
            };
        }
        //get all by workder id
        [HttpGet("{workerId}")]
        public async Task<IActionResult> getAll(Guid employeeid)
        {
            var workerGoals = list.Where(x => x.EmployeesId == employeeid).ToList();
            if (workerGoals.Count != 0) { return Ok(workerGoals); }
            return BadRequest("oopsie");
        }
        //get signle by object id
        [HttpGet("{id}")]
        public async Task<IActionResult> getSingle(Guid id)
        {
            var goalId = list.Select(x => x.Id).ToList();
            if (goalId.Contains(id)) { return Ok(list.First(x => x.Id == id)); }
            return BadRequest("oopsie");
        }

        [HttpPost("{workerId}/{subjectId}")]
        public async Task<IActionResult> AddGoal(Guid employeesId,Guid subjectId)
        {
            list.Add(new Goal(employeesId,subjectId));
        }
    }

}
