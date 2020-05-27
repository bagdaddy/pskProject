using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;

        }

        [HttpGet("{email}")]
        public async Task<IActionResult> SendByEmail(string email)
        {
            await _emailSender
                .SendEmailAsync(email, "Subject", "Text Message.", "Html Message")
                .ConfigureAwait(false);

            return Ok();
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
