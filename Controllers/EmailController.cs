using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audit.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Models.RequestModels;
using TP.Services;
using TP.Services.DTOServices.Models;

namespace TP.Controllers
{
    [Route("api/Emails")]
    [ApiController]
    [Audit]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IInviteControllerService _inviteControllerService;

        public EmailController(IEmailSender emailSender, IInviteControllerService inviteControllerService)
        {
            _emailSender = emailSender;
            _inviteControllerService = inviteControllerService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> SendByEmail(string email)
        {
            await _emailSender
                .SendEmailAsync(email, "Subject", "Text Message.", "Html Message")
                .ConfigureAwait(false);

            return Ok();
        }



        [HttpPost("{id}")]
        public async Task<IActionResult> SendById(Guid id, [FromBody]EmailRequestModel subjectRequestModel)
        {
            try
            {
                var inviteId = await _inviteControllerService.CreateInvite(id, subjectRequestModel);

                await _emailSender
                 .SendEmailAsync(subjectRequestModel.email, "Subject", "Text Message.", "localhost:5000/register/" + inviteId)
                 .ConfigureAwait(false);
                return Ok(inviteId);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SendToAdmin()
        {
            await _emailSender
                .SendAdminEmailAsync("Subject", "Text Message.")
                .ConfigureAwait(false);

            return Ok();
        }
        
    }
}
