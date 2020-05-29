using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Models.ResponseModels;

namespace TP.Controllers
{
    public class InviteController : ControllerBase
    {
        private readonly IEmployeesRepository _employeeRepository;
        private readonly IInviteRepository _inviteRepository;
        private readonly IMapper _mapper;
        public InviteController(IEmployeesRepository employeesRepository, IInviteRepository inviteRepository, IMapper mapper)
        {
            _employeeRepository = employeesRepository;
            _inviteRepository = inviteRepository;
            _mapper = mapper;
        }
        [HttpPost("api/Invite/{id}")]
        public async Task<IActionResult> CreateInvite(Guid id)
        {
            try
            {
                var employee = _employeeRepository.GetById(id);

                if(employee == null)
                {
                    return BadRequest("Employee does not exist");
                }

                var invite = new Invite(id);

                _inviteRepository.Add(invite);
                await _inviteRepository.SaveChanges();

                return Ok(invite.Id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("api/GetInvite/{id}")]
        public async Task<IActionResult> GetInvite(Guid id)
        {
            try
            {
                var invite = await _inviteRepository.GetById(id);

                var model = _mapper.Map<InviteResponseModel>(invite);

                return Ok(model);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
