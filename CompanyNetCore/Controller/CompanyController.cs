using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CompanyNetCore.Model;
using CompanyNetCore.Repo;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNetCore.Controllers
{
    [Route("api/Company")]
    public class CompanyController : ControllerBase
    {
        CompanyRepo CR = new CompanyRepo();
        [HttpGet]
        public IActionResult Read()
        {
            var result = CR.Read();
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public IActionResult ReadById(int Id)
        {
            var result = CR.ReadById(Id);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddOrUpdate([FromBody]Company company)
        {
            var result = CR.AddOrUpdate(company.Name, company.Business, company.Id);
            if (result == null)
                return NoContent();
            return Ok(result);
        }
    
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var result = CR.Delete(Id);
            if (result == null)
                return NoContent();
            return Ok(result);
            
        }
    }
}
