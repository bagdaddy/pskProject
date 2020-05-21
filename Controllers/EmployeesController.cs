using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP.Data.Entities;
using TP.DataContracts;
using TP.Services;
using TP.Services.DTOServices.Models;

namespace TP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeesControllerService _controllerService = new EmployeesControllerService();
        private readonly IDTOService _dtoService = new DTOService();

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<EmployeeDTO> Get()
        {
            List<Employee> employees = _controllerService.getAll();
            return _dtoService.EmployeesToDTO(employees);
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<EmployeeDTO> Get(Guid id)
        {
            Employee employee = _controllerService.getById(id);
            return _dtoService.EmployeeToDTO(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
