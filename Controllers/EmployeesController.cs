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
        private readonly IEmployeesControllerService _controllerService;
        private readonly IDTOService _dtoService = new DTOService();

        public EmployeesController(IEmployeesControllerService controllerService, IDTOService dtoService)
        {
            this._controllerService = controllerService;
            this._dtoService = dtoService;
        }
        // GET: api/Employees
        [HttpGet]
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            List<Employee> employees = _controllerService.GetAll();
            return _dtoService.employeesToDTO(employees);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public ActionResult<EmployeeDTO> GetEmployeeById(Guid id)
        {
            Employee employee = _controllerService.GetById(id);
            return _dtoService.employeeToDTO(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public void CreateEmployee([FromBody] EmployeeRequestModel model)
        {

        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public void UpdateEmployee(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void DeleteEmployee(int id)
        {
        }
    }
}
