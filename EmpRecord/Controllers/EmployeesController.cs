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

namespace EmpRecord.Controllers.V1
{
    
    [ApiController]
    //[ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepo _IemployeeRepo;
        private readonly ILogger<EmployeesController> logger;
        private readonly EmployeeHearderConfiguration employeeHeaderConfig;
        public EmployeesController(IEmployeeRepo IemployeeRepo, ILogger<EmployeesController> logger, IOptions<EmployeeHearderConfiguration> options)
        {
            _IemployeeRepo = IemployeeRepo;
            employeeHeaderConfig = options.Value;
            this.logger = logger;
        }
        //GET: api/v1/Employees/Json
        [HttpGet]
        [Route("Json")]
        public ActionResult<IEnumerable<EmployeeHearderConfiguration>> GetSettings()
        {

            return Content(JsonConvert.SerializeObject(employeeHeaderConfig));
        }

        //GET: api/v1/Employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
            {
            logger.LogInformation("Getting all the Employee");
            return _IemployeeRepo.GetAll();
            }

        //GET: api/v1/Employees/5
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
                return StatusCode(404, new { message = "Unidentified input" });
            }
        }

        //PUT: api/v1/Employees
        [HttpPut]
        public IActionResult PutEmployee(Employee employee)
        {
            //_IemployeeRepo.Entry(employee).State = EntityState.Modified;
            _IemployeeRepo.update(employee);
            try
            {
                _IemployeeRepo.save();
                return StatusCode(200, new { message = "Employee updated succesfully" });
            }
            catch (DbUpdateConcurrencyException ex)
            {   
                    return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });        
            }
         }

        // POST: api/v1/Employees

        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _IemployeeRepo.insert(employee);
                _IemployeeRepo.save();
            }
            return StatusCode(200, new { message = "Employee added Successfully" });  
        }

        // DELETE: api/v1/Employees/5
        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployee(int id)
        {
            Employee employee = _IemployeeRepo.getById(id);
            if (employee != null)
            {
                _IemployeeRepo.Delete(id);
                _IemployeeRepo.save();
                return StatusCode(200, new{ message = "Employee Deleted Successfully" });
            }
            else
            {
                return StatusCode(404, new { message = "Employee Id not found" });
            }
        }

    }

}
