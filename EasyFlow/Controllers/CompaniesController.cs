using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyFlow.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IGlobalValidationUtil _validate;
        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper
            mapper, IGlobalValidationUtil validate)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validate = validate;  
        }
        [HttpGet( )]
        public IActionResult GetCompanies()
        {
            var companies = _repository.company.GetAllCompanies(trackChanges: false);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            
            return Ok(companiesDto);

        }
        [HttpGet("{companyName}", Name = "CompanyById")]
        public IActionResult GetCompanyById(string companyName)
        {

            var company = _repository.company.GetCompanyFromName(companyName,trackChanges:false);
            if (company == null)
            {
                _logger.LogInfo($" Company with id {companyName} doesn't exist in the Database");
                return NotFound();
            }
            var companyDto = _mapper.Map<CompanyDto>(company);
            return Ok(companyDto);
        }
        [HttpPost("register")]
        public IActionResult RegisterCompany([FromBody] CompanyForCreationDto companyCreation)
        {
            if (companyCreation.CompanyName != null && companyCreation.CompanyName != null && companyCreation.CompanyCin != null && companyCreation.CompanyGstin != null && companyCreation.CompanyMail != null && companyCreation.CompanyPass != null && companyCreation.CompanyMobile != null)
            {
                if (!(_validate.IsEmailValid(companyCreation.CompanyMail)))
                {
                    _logger.LogError("Email is in valid");
                }
                var company = _repository.company.GetCompanyFromName(companyCreation.CompanyName, trackChanges: false);
                if (company == null)
                {
                    var companyEntity = _mapper.Map<company>(companyCreation);
                    _repository.company.CreateCompany(companyEntity);
                    _repository.Save();
                    var companyToReturn = _mapper.Map<CompanyRegistrationDto>(companyEntity);
                    return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id },
                   companyToReturn);
                }
                _logger.LogError("Commpany Name Already Registered");
                return BadRequest("Company Alreay Registered");
               
            }
            _logger.LogError("Fields can not be null in Company Registeration");
            return BadRequest("Fields can not be null in Company Registeration");
            
        }

        [HttpGet("login")]
        public IActionResult LoginCompanyByEmail([FromBody] LoginDto companyLogin)
        {
            if (companyLogin.Email != null && companyLogin.Mobile!=null && companyLogin.Pass !=null)
            {
                return StatusCode(405, "Now Allowed");
            }
            if (companyLogin.Mobile != null)
            {
                if (companyLogin.Pass != null)
                {
                   
                    var company = _repository.company.GetCompanyPasswordFromMobile(companyLogin.Mobile, trackChanges: false);

                    if (company != null)
                    {
                        if (companyLogin.Pass.Equals(company.CompanyPass))
                        {
                            return Ok($"Login Successful");
                        }
                        return BadRequest("Password Incorrect");

                    }
                    _logger.LogInfo($"Company with Mobile {companyLogin.Mobile} not found");
                    return BadRequest($"Company with Mobile {companyLogin.Mobile} not found");
                }
                _logger.LogError("Fields can not be null");
                return BadRequest("Fields can not be null");

            }
            else
            {
               
                if (companyLogin.Email != null && companyLogin.Pass != null)
                {
                    
                    if (!(_validate.IsEmailValid(companyLogin.Email)))
                    {
                        _logger.LogError("Company email is Invalid");
                        return BadRequest("Company email is Invalid");
                    }
                    var company = _repository.company.GetCompanyPasswordFromEmail(companyLogin.Email, trackChanges: false);

                    if (company != null)
                    {
                        if (companyLogin.Pass.Equals(company.CompanyPass))
                        {
                            return Ok($"Login Successful");
                        }
                        return BadRequest("Password Incorrect");

                    }
                    _logger.LogInfo($"Company with email {companyLogin.Email} not found");
                    return BadRequest($"Company with email {companyLogin.Email} not found");
                }
            }
            _logger.LogError("Fields can not be null");
            return BadRequest("Fields can not be null");
           

        }

        [HttpPost("request/{companyId}")]
        public IActionResult RequestAdminForWorker(string companyId, CompaniesRequestsDto companiesRequestsDto)
        {
            if (companiesRequestsDto.WorkerType != null || companiesRequestsDto.Location != null || companiesRequestsDto.Vacancy !=null)
            {
                var company = GetCompanyById(companyId);
                return BadRequest(company);
                
            }
            return BadRequest(companiesRequestsDto);

        }

    }
}
