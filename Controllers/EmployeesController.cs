using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Models.RequestModels;
using TP.Services;
using TP.Services.DTOServices.Models;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _repository;
        private readonly IDTOService _dtoService;
        private readonly UserManager<Employee> _userManager;

        public EmployeesController(IEmployeesRepository repository, IDTOService dtoService, UserManager<Employee> userManager)
        {
            _repository = repository;
            _dtoService = dtoService;
            _userManager = userManager;
        }
        // GET: api/Employees
        [HttpGet]
        public async Task <ActionResult<List<EmployeeDTO>>> GetEmployees()
        {
            try
            {
                List<Employee> employees = await _repository.GetAll();
                return _dtoService.EmployeesToDTO(employees);
            } catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed getting employees");
            }
            
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(Guid id)
        {
            Employee employee = await _repository.GetById(id);
            return _dtoService.EmployeeToDTO(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult> CreateEmployee([FromBody] EmployeeRequestModel model)
        {
            Employee employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                LearnedSubjects = new List<EmployeeSubject>()
            };
            try
            {
                await _repository.CreateEmployee(employee);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeRequestModel request)
        {
            try
            {
                Employee employee = await _repository.UpdateEmployee(id, request);

                if (request.Email != null && request.Email.ToUpperInvariant() != employee.Email.ToUpperInvariant())
                {
                    employee = await UpdateCredentials(employee, request.Email);
                }

                return _dtoService.EmployeeToDTO(employee);
            }
            catch (Exception exception)
            {
                return BadRequest("Failed to update employee");
            }
        }

        private async Task<Employee> UpdateCredentials(Employee employee, string updatedEmail)
        {
            //Update username too
            string updatedName = updatedEmail.Replace("@", "").Replace(".", "").Replace("-", "");
            employee.UserName = updatedName;
            employee.Email = updatedEmail;
            var response = await _userManager.UpdateAsync(employee);

            if (!response.Succeeded)
            {
                throw new InvalidOperationException("Employee email could not be updated.");
            }
            return employee;
        }
    }
}
