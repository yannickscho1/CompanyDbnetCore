using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CompanyNetCore.Model;
using CompanyNetCore.Repo;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNetCore.Controllers
{
    [Route("api/Empolyee")]
    public class EmployeeController : ControllerBase
    {
        EmployeeRepo ER = new EmployeeRepo();
        [HttpGet]
        public IActionResult Get()
        {
            var result = ER.Get();
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var result = ER.GetById(Id);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody]Employee employee)
        {
            var result = ER.AddOrUpdate(employee.Vorname, employee.Name, employee.Salary, employee.Gender, employee.Id);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody]Employee employee)
        {
            var result = ER.AddOrUpdate(employee.Vorname, employee.Name, employee.Salary, employee.Gender, employee.Id);
            if (result == null)
                return NoContent();
            return Ok(result);
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var result = ER.Delete(Id);
            var resval = result == null ? (IActionResult) NoContent() : Ok(result); 
            return resval;

        }
    }
}
