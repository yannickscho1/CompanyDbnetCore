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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using TobitWebApiExtensions.Extensions;
using TobitLogger.Core;
using TobitLogger.Core.Models;

namespace CompanyNetCore.Controllers
{
    [Route("api/Company")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyRepository _companyRepo;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(CompanyRepository companyRepo, ILoggerFactory logger)
        {
            _companyRepo = companyRepo;
            _logger = logger.CreateLogger<CompanyController>();
        }
        [HttpGet]
        public IActionResult Get()
        {
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
                        _logger.Error(new ExceptionData(ex));
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
                        _logger.Error(new ExceptionData(ex));
                        return StatusCode(StatusCodes.Status409Conflict);
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

        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Add([FromBody]CompanyDto company)
        {
            Company retVal;
            var _user = HttpContext.GetTokenPayload<Auth.Models.LocationUserTokenPayload>();
            var groups = HttpContext.GetUacGroups();
            if (_user.SiteId == "77890-29567")
            {
                try
                {
                    retVal = _companyRepo.Create(company);
                }
                catch (Helper.RepoException<CreateResultType> ex)
                {
                    switch (ex.Type)
                    {
                        case CreateResultType.SQLERROR:
                            _logger.Error(new ExceptionData(ex));
                            return StatusCode(StatusCodes.Status409Conflict);
                        case CreateResultType.INVALIDEARGUMENT:
                            _logger.Error(new ExceptionData(ex));
                            return StatusCode(StatusCodes.Status409Conflict);
                        case CreateResultType.ERROR:
                            _logger.Error(new ExceptionData(ex));
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
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Update([FromBody]CompanyDto company, int id)
        {
            var _user = HttpContext.GetTokenPayload<Auth.Models.LocationUserTokenPayload>();
            var groups = HttpContext.GetUacGroups();
            if (_user.SiteId == "77890-29567")
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
                        case UpdateResultType.SQLERROR:
                            _logger.Error(new ExceptionData(ex));
                            return StatusCode(StatusCodes.Status409Conflict);
                        case UpdateResultType.ERROR:
                            _logger.Error(new ExceptionData(ex));
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
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var _user = HttpContext.GetTokenPayload<Auth.Models.LocationUserTokenPayload>();
            var groups = HttpContext.GetUacGroups();
            if (_user.SiteId == "77890-29567")
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
                            _logger.Error("SQL Error", new ExceptionData(ex));
                            return StatusCode(StatusCodes.Status409Conflict);
                        case DeleteResultType.ERROR:
                            _logger.Error("Error", new ExceptionData(ex));
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
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
