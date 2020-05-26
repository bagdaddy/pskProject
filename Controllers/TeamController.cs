using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Services;
using TP.Services.DTOServices.Models;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {

        private readonly ITeamRepository _teamRepository;
        private readonly IDTOService _dtoService;

        public TeamController(ITeamRepository teamRepository, IDTOService dtoService)
        {
            _teamRepository = teamRepository;
            _dtoService = dtoService;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<List<TeamDTO>>> GetTeams()
        {
            List<Team> teams = await _teamRepository.GetAllTeams();
            //return _dtoService.TeamsToDTO(teams);
            return Ok(teams);
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeamById(Guid id)
        {
            Team team = await _teamRepository.GetTeamById(id);
            //return _dtoService.TeamToDTO(team);
            return Ok(team);
        }

        /*// POST: api/Team
        [HttpPost]
        public async Task<ActionResult> CreateTeam([FromBody] TeamRequestModel model)
        {
            Team team = new Team()
            {
                Id = Guid.NewGuid(),
                ChildTeams = new List<Team>(),
                TeamLeader = model.
            };
            try
            {
                await _repository.CreateTeam(team);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }*/

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public void UpdateTeam(Guid id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(Guid id)
        {
            await _teamRepository.DeleteTeam(id);
            return Ok();
        }
    }
}
