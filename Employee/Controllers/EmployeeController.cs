using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee.ServiceLayer;
using Employee.ServiceLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService service;

        public EmployeeController(IEmployeeService service) => this.service = service;

        [HttpPost("[action]")]
        public EmployeeDto ChangeEmployeeStatus(string id)
        {
            return service.ChangeEmployeeStatus(id);
        }

        [HttpPut("[action]")]
        public IActionResult Create([FromForm]EmployeeDto dto)
        {
            try
            {
                return Json(service.Create(dto));
            }
            catch (Exception)
            {
                return Conflict(new { message = string.Format("There is already an employee with ID {0}", dto.EmployeeId) });
            }
        }

        [HttpGet("[action]")]
        public IEnumerable<EmployeeDto> GetAllActiveEmployees()
        {
            return service.GetAllActiveEmployees();
        }

        [HttpGet("[action]")]
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return service.GetAllEmployees();
        }

        [HttpGet("[action]")]
        public EmployeeDto GetEmployee(string id)
        {
            return service.GetEmployee(id);
        }
    }
}
