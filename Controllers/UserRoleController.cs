using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Audit.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using TP.Mappings.EntityToResponse;
using TP.Models.ResponseModels;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [Audit]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleController(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        [Route("{id}/edit")]
        [HttpPut]
        [Consumes("application/json")]
        public async Task<IActionResult> EditUserRole([FromBody]UserRoleRequestModel userRoleRequestModel, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userRole = await _userRoleRepository.GetById(id);
                userRole.Name = userRoleRequestModel.Name;
                _userRoleRepository.UpdateUserRole(userRole);
                await _userRoleRepository.SaveChanges();
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateUserRole([FromBody]UserRoleRequestModel userRoleRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var userRole = new UserRole(userRoleRequestModel.Name);
                _userRoleRepository.AddUserRole(userRole);
                await _userRoleRepository.SaveChanges();
                return Ok(UserRoleResponseModel.ToResponseModel(userRole));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}/delete")]
        public async Task<IActionResult> DeleteUserRole(Guid id)
        {
            try
            {
                var userRole = await _userRoleRepository.GetById(id);
                if(userRole == null)
                {
                    return BadRequest("User role doesn't exist");
                }
                if (userRole.EmployeeList.Any())
                {
                    return BadRequest("There are employees that belong to this group!");
                }
                _userRoleRepository.DeleteUserRole(userRole);
                await _userRoleRepository.SaveChanges();
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllUserRoles()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var userRoles = await _userRoleRepository.GetAll();
                return Ok(UserRoleResponseModel.ToResponseModelList(userRoles));
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{employeeId}/getAll")]
        public async Task<IActionResult> GetUserRolesByEmployeeId(Guid employeeId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var userRoles = await _userRoleRepository.GetAllByEmployeeId(employeeId);
                return Ok(UserRoleResponseModel.ToResponseModelList(userRoles));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
