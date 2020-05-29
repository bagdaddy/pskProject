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
        private readonly IInviteRepository _inviteRepository;
        private readonly IMapper _mapper;
        public InviteController(IInviteRepository inviteRepository, IMapper mapper)
        {
            _inviteRepository = inviteRepository;
            _mapper = mapper;
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
