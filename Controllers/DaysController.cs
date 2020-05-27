using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Services;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {
        private readonly IDaysRepository _repository;
        private readonly IDTOService _dtoService = new DTOService();
        public DaysController(IDaysRepository repository, IDTOService dtoService)
        {
            this._repository = repository;
            this._dtoService = dtoService;
        }

        [HttpGet("{workerId}")]
        public async Task<IActionResult> getAll(Guid employeeid)
        {
            List<Day> days = await _repository.GetAll(employeeid);
        }

        //get signle by object id
        [HttpGet("{id}")]
        public async Task<IActionResult> getSingle(Guid id)
        {
            Day day = await _repository.GetSingle(id);
        }

        //get signle by worker id and date
        [HttpGet("{workerId}/{date}")]
        public async Task<IActionResult> getSingle(Guid employeeid, DateTime date)
        {
            Day day = await _repository.GetSingle(employeeid,date);
        }

        //count days by worker id and quarter
        [HttpGet("{workerId}/{quarter}")]
        public async Task<IActionResult> getDatesThisQuarter(Guid employeeid, int quarter)
        {
            int dayCount = await _repository.GetThisQuarter(employeeid,quarter);
        }
        [HttpGet("{workerId}/{quarter}")]
        public async Task<IActionResult> GetEmployeesBySubject(Guid subjectId)
        {
            List<Guid> employeesId = await _repository.GetEmployeesBySubject(subjectId);
        }

        // DELETE by object id
        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteDateAsync(Guid id)
        {
            await _repository.Delete(id);
            return Ok("Deleted maybe");
        }
    }
}