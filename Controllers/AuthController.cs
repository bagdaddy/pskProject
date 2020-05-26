using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;
using TP.Models.RequestModels;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;

        public AuthController(SignInManager<Employee> signInManager, UserManager<Employee> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        [Consumes("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            try
            {
                // username = email without special symbols
                string username = login.Email.Replace("@", "").Replace(".", "").Replace("-", "");

                var result = await _signInManager.PasswordSignInAsync(username, login.Password, login.RememberMe, false);

                if (!result.Succeeded)
                {
                    Console.WriteLine("User -> " + login.Email + "  login failed.");
                    //Log.Information("Login failed");
                    return Conflict("Login failed");
                }

                Console.WriteLine("User -> " + login.Email + "  logged in successfully!");
                //Log.Information("Logged in successfully");
                return Ok("Success");
            }
            catch (Exception exception)
            {
                //Log.Error(exception, "Failed to login");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to log in" + exception.Message);
            }
        }

        [HttpGet("role")]
        public async Task<ActionResult<string>> GetRole()
        {
            //if (User.Identity.IsAuthenticated == true)
           try {
                // var user = await _userManager.FindByEmailAsync(User.Identity.Name);
               // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
               // var userId2 = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.GetUserAsync(User);
                //var user2 = await _userManager.GetUserAsync(HttpContext.User);
                var roles = await _userManager.GetRolesAsync(user);
                

                return roles.FirstOrDefault();
            } catch (Exception exception)
            {
                //Log.Error(exception, "Failed to get role");
                return StatusCode((int)HttpStatusCode.Unauthorized, "User ir not logged in.");
            }
            
        }

        [HttpPost("register")]
        [Consumes("application/json")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountRequest registerAccount)
        {
            // username = email without special symbols
            Employee user = new Employee
            {
                UserName = registerAccount.Email.Replace("@", "").Replace(".", "").Replace("-", ""),
                Email = registerAccount.Email,
                FirstName = registerAccount.FirstName,
                LastName = registerAccount.LastName,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerAccount.Password);

                if (!result.Succeeded)
                {
                    Console.WriteLine("User -> " + registerAccount.Email + "  registration failed.");
                    //Log.Information("Registering user was unsuccessfull");
                    return Conflict(result.Errors);
                }

                Console.WriteLine("User -> " + registerAccount.Email + "  registered successfully!");
                //Log.Information("Registration succeeded");
                return Ok(result.Succeeded);
            }
            catch (Exception exception)
            {
                //Log.Error(exception, "Failed to register user");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to register user. Message: " + exception.Message);
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();

                Console.WriteLine("User logged out.");
                return NoContent();
            }
            catch (Exception exception)
            {
                //Log.Error(exception, "Log out failed");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Log out failed. Message: " + exception.Message);
            }
        }
    }
}
