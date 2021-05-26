using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpRecord.Models;
using System.Net;
using EmpRecord.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EmpRecord.Controllers.V2
{

    [ApiController]
    //[ApiVersion("2.0")]
    [Route("api/v2/[controller]")]

    public class EmployeesV2Controller : Controller
    {
        private readonly IEmployeeRepo _IemployeeRepo;
        private readonly ILogger<EmployeesV2Controller> logger;
        public EmployeesV2Controller(IEmployeeRepo IemployeeRepo, ILogger<EmployeesV2Controller> logger)
        {
            _IemployeeRepo = IemployeeRepo;
            this.logger = logger;
        }

        //GET: api/Employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            logger.LogInformation("Getting all the Employee");
            return _IemployeeRepo.GetAll();
        }
        [HttpGet]
        [Route("Salary")]
        public ActionResult<IEnumerable<Employee>> GetSalaryInfo()
        {
            return _IemployeeRepo.GetSalaryOrder();
        }
        [HttpGet]
        [Route("Developer")]
        public ActionResult<IEnumerable<Employee>> DeveloperInfo()
        {
            return _IemployeeRepo.Developer();
        }
        [HttpGet]
        [Route("Designer")]
        public ActionResult<IEnumerable<Employee>> DesignerInfo()
        {
            return _IemployeeRepo.Designer();
        }
        [HttpGet]
        [Route("marketing")]
        public ActionResult<IEnumerable<Employee>> MarketingInfo()
        {
            return _IemployeeRepo.Marketing();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            logger.LogDebug("Executing Get by Id method");
            var employee = _IemployeeRepo.getById(id);
            if (employee != null)
            {
                return employee;
            }
            else
            {
                logger.LogWarning("Id not found");
                return StatusCode(404, "Unidentified input");
            }
        }

        //PUT: api/Employees
        [HttpPut]
        public IActionResult PutEmployee(Employee employee)
        {
            //_IemployeeRepo.Entry(employee).State = EntityState.Modified;
            _IemployeeRepo.update(employee);
            try
            {
                _IemployeeRepo.save();
                return StatusCode(200, "Updated Successfully");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        // POST: api/Employees

        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _IemployeeRepo.insert(employee);
                _IemployeeRepo.save();
            }
            return StatusCode(200, "Employee Added");
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            Employee employee = _IemployeeRepo.getById(id);
            if (employee != null)
            {
                _IemployeeRepo.Delete(id);
                _IemployeeRepo.save();
                return StatusCode(200, "Deleted Successfully");
            }
            else
            {
                return StatusCode(404, "Entered Id not found");
            }
        }

    }

}
