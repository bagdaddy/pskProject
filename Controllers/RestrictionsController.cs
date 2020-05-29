using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.DataContracts;
using TP.Models.RequestModels;

namespace TP.Controllers
{
    public class RestrictionsController : ControllerBase
    {
        private readonly IRestrictionRepository _restrictionRepository;
        public RestrictionsController(IRestrictionRepository restrictionRepository)
        {
            _restrictionRepository = restrictionRepository;
        }
        [HttpPut("api/GlobalRestrict")]
        public async Task<IActionResult> GlobalRestrict([FromBody]GlobalRestrictionRequestModel requestModel)
        {
            try
            {
                var employees = await _restrictionRepository.GetAll();

                foreach(var employee in employees)
                {
                    if(!requestModel.GlobalDayLimit.HasValue)
                    {
                        return BadRequest("Missing global day limit value");
                    }
                    employee.GlobalDayLimit = requestModel.GlobalDayLimit.Value;

                    _restrictionRepository.Update(employee);
                }

                await _restrictionRepository.SaveChanges();

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("api/LocalRestrict")]
        public async Task<IActionResult> LocalRestrict([FromBody] LocalRestrictionRequestModel requestModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var employee = await _restrictionRepository.GetById(requestModel.EmployeeId);

                if(employee == null)
                {
                    return BadRequest("No such employee exists");
                }

                employee.LocalDayLimit = requestModel.LocalDayLimit;

                _restrictionRepository.Update(employee);
                await _restrictionRepository.SaveChanges();

                return Ok();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("api/LocalRestrictRemove/{id}")]
        public async Task<IActionResult> LocalRestrictRemove(Guid id)
        {
            try
            {
                var employee = await _restrictionRepository.GetById(id);

                if (employee == null)
                {
                    return BadRequest("No such employee exists");
                }

                employee.LocalDayLimit = null;

                _restrictionRepository.Update(employee);
                await _restrictionRepository.SaveChanges();

                return Ok();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("api/GetRestriction/{id}")]
        public async Task<IActionResult> GetRestriction(Guid id)
        {
            try
            {
                var employee = await _restrictionRepository.GetById(id);
                
                if(employee == null)
                {
                    return BadRequest("Employee does not exist");
                }

                if(!employee.LocalDayLimit.HasValue)
                {
                    return Ok(employee.GlobalDayLimit);
                }
                if(employee.LocalDayLimit <= employee.GlobalDayLimit)
                {
                    return Ok(employee.LocalDayLimit);
                }

                return Ok(employee.GlobalDayLimit);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
