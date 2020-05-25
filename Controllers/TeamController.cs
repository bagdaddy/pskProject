using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        private readonly ITeamControllerService _controllerService = new TeamControllerService();
        private readonly IDTOService _dtoService = new DTOService();

        // GET: api/Team
        [HttpGet]
        public ActionResult<List<TeamDTO>> GetTeams()
        {
            List<Team> teams = _controllerService.getAll();
            return _dtoService.TeamsToDTO(teams);
        }

        // GET: api/Team/5
        [HttpGet("{id}")]
        public ActionResult<TeamDTO> GetTeamById(Guid id)
        {
            Team team = _controllerService.getById(id);
            return _dtoService.TeamToDTO(team);
        }

        // POST: api/Team
        [HttpPost]
        public void CreateTeam([FromBody] string value)
        {
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public void UpdateTeam(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteTeam(int id)
        {
        }
    }
}
