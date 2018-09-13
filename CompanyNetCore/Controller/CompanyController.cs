﻿using System;
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
        public IActionResult Get()
        {
            var result = CR.Get();
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var result = CR.GetById(Id);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody]Company company)
        {
            var result = CR.AddOrUpdate(company.Name, company.Business, company.Id);
            if (result == null)
                return NoContent();
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody]Company company)
        {
            var result = CR.AddOrUpdate(company.Name, company.Business, company.Id);
            if (result == null)
                return NoContent();
            return Ok(result);
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var result = CR.Delete(Id);
            if (result == null)
                return NoContent();
            return Ok(result);
            
        }
    }
}
