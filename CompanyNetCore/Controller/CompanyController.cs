using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CompanyNetCore.Model;
using CompanyNetCore.Repo;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CompanyNetCore.Helper;
using CompanyNetCore.Interface;
using CompanyNetCore.Model.dto;

namespace CompanyNetCore.Controllers
{
    [Route("api/Company")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyRepository _companyRepo;

        public CompanyController(CompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var header = Request.Headers["Authorization"].ToString().Split(' ')[1];
            var auth = Authentication.Authenticat(header);
            if(auth == false)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            List<Company> retVal;
            try
            {
                retVal = _companyRepo.Read();
            }
            catch (RepoException<ReadResultType> ex)
            {
                switch (ex.Type)
                {
                    case ReadResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    default:
                        break;
                }
                return BadRequest();
                throw;
            }
            if (retVal == null)
                return StatusCode(StatusCodes.Status204NoContent);
            return StatusCode(StatusCodes.Status200OK, retVal);
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            CompanyDto retVal;
            try
            {
                retVal = _companyRepo.ReadById(id);
            }
            catch (RepoException<ReadResultType> ex)
            {
                switch (ex.Type)
                {
                    case ReadResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    case ReadResultType.NOTFOUND:
                        return StatusCode(StatusCodes.Status204NoContent);
                    default:
                        break;
                }
                return StatusCode(StatusCodes.Status204NoContent);
                throw;
            }
            if (retVal == null)
                return StatusCode(StatusCodes.Status204NoContent);
            return StatusCode(StatusCodes.Status200OK, retVal);
        }

        [HttpPost]
        public IActionResult Add([FromBody]CompanyDto company)
        {
            Company retVal;
            try
            {
                retVal = _companyRepo.Create(company);
            }
            catch (Helper.RepoException<CreateResultType> ex)
            {
                switch (ex.Type)
                {
                    case CreateResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    case CreateResultType.INVALIDEARGUMENT:
                        return StatusCode(StatusCodes.Status409Conflict);
                    case CreateResultType.ERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    default:
                        break;
                }
                return BadRequest();
                throw;
            }
            if (retVal == null)
                return StatusCode(StatusCodes.Status204NoContent);
            return StatusCode(StatusCodes.Status200OK, retVal);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody]CompanyDto company, int id)
        {
            Company retVal;
            try
            {
                retVal = _companyRepo.Update(company, id);
            }
            catch (RepoException<UpdateResultType> ex)
            {
                switch (ex.Type)
                {
                    case Helper.UpdateResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    case Helper.UpdateResultType.ERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    default:
                        break;
                }
                return BadRequest();
                throw;
            }
            if (retVal == null)
                return StatusCode(StatusCodes.Status204NoContent);
            return StatusCode(StatusCodes.Status200OK, retVal);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Company retVal;
            try
            {
                retVal = _companyRepo.Delete(id);
            }
            catch (RepoException<DeleteResultType> ex)
            {
                switch (ex.Type)
                {
                    case DeleteResultType.SQLERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    case DeleteResultType.ERROR:
                        return StatusCode(StatusCodes.Status409Conflict);
                    default:
                        break;
                }
                return BadRequest();
                throw;
            }
            if (retVal.Id == null)
                return StatusCode(StatusCodes.Status204NoContent);
            return StatusCode(StatusCodes.Status200OK, retVal);

        }
    }
}
