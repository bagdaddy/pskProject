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
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _repository;
        private readonly IDTOService _dtoService = new DTOService();

        public EmployeesController(IEmployeesRepository repository, IDTOService dtoService)
        {
            this._repository = repository;
            this._dtoService = dtoService;
        }
        // GET: api/Employees
        [HttpGet]
        public async Task <ActionResult<List<EmployeeDTO>>> GetEmployees()
        {
            List<Employee> employees = await _repository.GetAll();
            return _dtoService.EmployeesToDTO(employees);
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
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployee([FromBody] UpdateEmployeeRequestModel request)
        {
            try
            {
                Employee employee = await _repository.UpdateEmployee(request);

                return _dtoService.EmployeeToDTO(employee);
            }
            catch (Exception exception)
            {
                return BadRequest("Failed to update employee");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            await _repository.Delete(id);
            return Ok("Deleted maybe");
        }
    }
}
