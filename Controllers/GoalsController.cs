using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Models.RequestModels;
using TP.Models.ResponseModels;

namespace TP.Controllers
{
    public class GoalsController:ControllerBase
    {
        private readonly IGoalsRepository _repository;
        private readonly IEmployeesRepository _employeeRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamControllerService _teamControllerService;
        private readonly IMapper _mapper;
        public GoalsController(IGoalsRepository repository, IEmployeesRepository employeesRepository, ISubjectRepository subjectRepository, IMapper mapper, ITeamRepository teamRepository, ITeamControllerService teamControllerService)
        {
            _repository = repository;
            _employeeRepository = employeesRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _teamRepository = teamRepository;
            _teamControllerService = teamControllerService;
        }
        [HttpGet("/api/Goals/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var goal = await _repository.GetByIdWithMembers(id);

                var list = _mapper.Map<List<GoalsResponseModel>>(goal);

                return Ok(list);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("/api/GetAllGoals/{id}")]
        public async Task<IActionResult> GetAllById(Guid id)
        {
            try
            {
                var team = await _teamRepository.GetById(id);
                var list = new List<Employee>();

                await _teamControllerService.GetAllTeams(team, list);

                List<GoalsTeamResponseModel> model = _mapper.Map<List<GoalsTeamResponseModel>>(list);

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/api/Goals")]
        [Consumes("application/json")]
        public async Task<IActionResult> AddGoal([FromBody] GoalRequestModel model)
        {
            try
            {
                var employee = await _employeeRepository.GetById(model.EmployeeId);

                if (employee == null)
                {
                    return BadRequest("Employee does not exist");
                }

                var subject = await _subjectRepository.GetById(model.SubjectId);

                if (subject == null)
                {
                    return BadRequest("Subject does not exist");
                }

                var goal = new Goal(model.EmployeeId, model.SubjectId);

                _repository.AddGoal(goal);
                await _repository.SaveChanges();

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }

}
