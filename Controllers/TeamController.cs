using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Models.RequestModels;
using TP.Models.ResponseModels;
using TP.Services;
using TP.Services.DTOServices.Models;

namespace TP.Controllers
{
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamControllerService _teamControllerService;
        private readonly IMapper _mapper;
        public TeamController(ITeamRepository teamRepository, ITeamControllerService teamControllerService, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _teamControllerService = teamControllerService;
            _mapper = mapper;
        }
        [HttpGet("api/GetTeams")]
        public async Task<IActionResult> GetTeams()
        {
            try
            {
                var teams = await _teamRepository.GetAll();
                var teamListHierarchy = new List<Employee>();

                foreach (var team in teams)
                {
                    if (!team.BossId.HasValue)
                    {
                        await _teamControllerService.GetAllTeams(team, teamListHierarchy);
                    }
                }

                List<TeamResponseModel> teamResponseModel = _mapper.Map<List<TeamResponseModel>>(teamListHierarchy);

                return Ok(teamResponseModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("api/GetTeams/{id}")]
        public async Task<IActionResult> GetTeamsById(Guid id)
        {
            try
            {
                var team = await _teamRepository.GetById(id);
                var list = new List<Employee>();

                await _teamControllerService.GetAllTeams(team, list);

                List<TeamResponseModel> teamResponseModel = _mapper.Map<List<TeamResponseModel>>(list);

                return Ok(teamResponseModel);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("api/UpdateTeamMember")]
        public async Task<IActionResult> UpdateTeamMember([FromBody]UpdateTeamRequestModel teamRequestModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var employee = await _teamRepository.GetById(teamRequestModel.Id);

                if(employee == null)
                {
                    return BadRequest("Employee does not exist");
                }

                var boss = await _teamRepository.GetById(teamRequestModel.BossId);

                if(boss == null)
                {
                    return BadRequest("Given new boss does not exist");
                }

                await _teamRepository.UpdateTeam(teamRequestModel);

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("api/DeleteFromTeam/{id}")]
        public async Task<IActionResult> DeleteFromTeam(Guid id)
        {
            try
            {
                var employee = await _teamRepository.GetById(id);

                if(employee == null)
                {
                    return BadRequest("No employee found");
                }
                if(employee.Subordinates.Any())
                {
                    return BadRequest("Cannot remove employee that has subordinates");
                }

                _teamRepository.Delete(employee);
                await _teamRepository.SaveChanges();

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
