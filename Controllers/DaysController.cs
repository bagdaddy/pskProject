using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Models.RequestModels;
using TP.Services;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {
        private readonly IDaysRepository _repository;
        private readonly IDTOService _dtoService;
        public DaysController(IDaysRepository repository, IDTOService dtoService)
        {
            _repository = repository;
            _dtoService = dtoService;
        }

        [HttpGet("GetDayByEmployeeId/{employeeId}")]
        public async Task<IActionResult> getAll(Guid employeeId)
        {
            List<Day> days = await _repository.GetAll(employeeId);
            return Ok(days);
        }

        //get signle by object id
        [HttpGet("GetDayById/{id}")]
        public async Task<IActionResult> getSingle(Guid id)
        {
            try
            {
                Day day = await _repository.GetSingle(id);
                return Ok(day);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        //get signle by worker id and date
        [HttpGet("GetDayByDate/{employeeId}/{date}")]
        public async Task<IActionResult> getSingle(Guid employeeId, DateTime date)
        {
            try
            {
                Day day = await _repository.GetSingle(employeeId, date);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound("Failed to get day with error: " + e.Message);
            }
        }

        //count days by worker id and quarter
        [HttpGet("GetDaysInQuarter/{employeeId}/{quarter}")]
        public async Task<IActionResult> getDatesThisQuarter(Guid employeeId, int quarter)
        {
            try
            {
                int dayCount = await _repository.GetThisQuarter(employeeId, quarter);
                return Ok(dayCount);
            }
            catch(Exception e)
            {
                // log exception
                return NotFound("failed to get days with error: " + e.Message);
            }
        }
        // isveda darbuotoju sarasa pagal subject id
        [HttpGet("GetEmployeesBySubjectId/{subjectId}")]
        public async Task<IActionResult> GetEmployeesBySubject(Guid subjectId)
        {
            try
            {
                List<Employee> employees = await _repository.GetEmployeesBySubject(subjectId);
                return Ok(_dtoService.EmployeesToDTO(employees));
            }
            catch (Exception e)
            {
                return NotFound("Failed to get employees with error: " + e.Message);
            }
        }

        //CREATE new Day
        [HttpPost("CreateDay")]
        [Consumes("application/json")]
        public async Task<ActionResult> CreateDay([FromBody] DayRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _repository.Create(model);
                return Ok(model);
            }
            catch (Exception e)
            {
                // log error
                return BadRequest("Error occured: " + e.Message);
            }
        }

        // DELETE by object id
        [HttpDelete("DeleteDay/{id}")]
        public async Task<ActionResult> deleteDateAsync(Guid id)
        {
            try
            {
                await _repository.Delete(id);
                return Ok("Deleted day with id: " + id);
            }
            catch (Exception e)
            {
                //Log exception
                return NotFound("Failed to delete day with error: " + e.Message);
            }
        }
    }
}