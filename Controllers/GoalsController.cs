using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Models.RequestModels;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController:ControllerBase
    {
        private readonly IGoalsRepository _repository;
        public GoalsController(IGoalsRepository repository)
        {
            this._repository = repository;
        }
        //get all by workder id
        [HttpGet("{workerId}")]
        public async Task<IActionResult> getAll(Guid employeeid)
        {
            try
            {
                List<Goal> workerGoals = await _repository.GetAll(employeeid);
                return Ok(); //papildyti kur grazinti
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        //get signle by object id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Goal goal = await _repository.GetById(id);
            return Ok();//papildyti kur grazinti
        }

        [HttpPost("{workerId}/{subjectId}")]
        public async Task<IActionResult> AddGoal([FromBody] GoalRequestModel model)
        {
            Goal goal = new Goal()
            {
                Id = Guid.NewGuid(),
                Employee = model.Employee,
                Subject = model.Subject
            };
            try
            {
                await _repository.AddGoal(goal);
                return Ok(); // papildyti kur grazinti
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }

}
