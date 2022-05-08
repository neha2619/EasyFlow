using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpGet(Name ="companyById")]
        public IActionResult GetCompanies()
        {
            var companies = _repository.company.GetAllCompanies(trackChanges: false);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

            return Ok(companiesDto);

        }
      
        [HttpGet("{companyName}")]
        public IActionResult GetCompanyByName(string companyName)
        {

            var company = _repository.company.GetCompanyFromName(companyName, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($" Company with Name {companyName} doesn't exist in the Database");
                return NotFound();
            }
            var companyDto = _mapper.Map<CompanyDto>(company);
            return Ok(companyDto);
        }
        
        [HttpGet("byid/{companyId}")]
        public IActionResult GetCompanyById(Guid companyId)
        {

            var company = _repository.company.GetCompanyFromId(companyId, trackChanges: false);
            if (company != null)
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return NoContent();
            }
            
            _logger.LogInfo($" Company with id {companyId} doesn't exist in the Database");
            return BadRequest();
        }


        [HttpPost("register")]
        public IActionResult RegisterCompany([FromBody] CompanyForCreationDto companyCreation)
        {
            if (companyCreation.CompanyName != "" && companyCreation.CompanyName != "" && companyCreation.CompanyCin != "" && companyCreation.CompanyGstin != "" && companyCreation.CompanyMail != "" && companyCreation.CompanyPass != "" && companyCreation.CompanyMobile != "")
            {

                if (!(_validate.IsEmailValid(companyCreation.CompanyMail)))
                {
                    _logger.LogInfo("Email is invalid ");
                    return BadRequest("Invalid Email");
                }
                if (!(_validate.IsCinValid(companyCreation.CompanyCin)))
                {
                    _logger.LogError("Company Identification Number must be of 21 characters");
                    return BadRequest("Company Identification Number must be of 21 characters");
                }
                if (!(_validate.IsGstinValid(companyCreation.CompanyGstin)))
                {
                    _logger.LogError("Company GST Number must be of 16 characters");
                    return BadRequest("Company GST Number must be of 15 digits");
                }
                if (!(_validate.IsMobileValid(companyCreation.CompanyMobile)))
                {
                    _logger.LogError("Company Mobile Number is Not Valid");
                    return BadRequest("Invalid Mobile");
                }
                if (!(_validate.IsPasswdStrong(companyCreation.CompanyPass)))
                {

                    _logger.LogError("Password is too weak");
                    return BadRequest("Password is too weak");
                }



                var company = _repository.company.GetCompanyFromName(companyCreation.CompanyName, trackChanges: false);
                if (company != null)
                {
                    _logger.LogError("Commpany Name Already Registered");
                    return BadRequest("Company Already Registered");

                }
                var companyEntity = _mapper.Map<company>(companyCreation);
                _repository.company.CreateCompany(companyEntity);
                _repository.Save();
                var companyToReturn = _mapper.Map<CompanyRegistrationDto>(companyEntity);
                return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id },
               companyToReturn);
            }
            _logger.LogError("Fields can not be null in Company Registeration");
            return BadRequest("Fields can not be null in Company Registeration");

        }

        [HttpGet("login")]
        public IActionResult LoginCompanyByEmail([FromBody] LoginDto companyLogin)
        {
            if (companyLogin.Email != null && companyLogin.Mobile != null && companyLogin.Pass != null)
            {
                return StatusCode(405, "Now Allowed");
            }
            if (companyLogin.Mobile != null)
            {

                if (companyLogin.Pass != null)
                {
                    if (!(_validate.IsMobileValid(companyLogin.Mobile)))
                    {
                        _logger.LogError("Company Mobile Number is Not Valid");
                        return BadRequest("Invalid Mobile");
                    }

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
                        _logger.LogInfo("Email is invalid ");
                        return BadRequest("Invalid Email");
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
        public IActionResult RequestAdminForWorker(Guid companyId, CompaniesRequestsDto companiesRequestsDto)
        {
            if (companiesRequestsDto.WorkerType != null || companiesRequestsDto.Location != null || companiesRequestsDto.Vacancy != null)
            {
                var company = GetCompanyById(companyId);
                if (company != null)
                {
                    if (_validate.IsNumberValid(companiesRequestsDto.Vacancy) && _validate.IsStringValid(companiesRequestsDto.WorkerType))
                        if (Convert.ToInt64(companiesRequestsDto.Vacancy) > 0)
                        {
                            companiesRequestsDto.CompanyID = companyId;
                            var requestEntity = _mapper.Map<AdminCompany>(companiesRequestsDto);
                            //_logger.LogInfo(requestEntity.Vacancy.ToString());
                            //REMOVE THIS LINE
                            _repository.AdminCompany.CreateRequest(requestEntity);
                            _repository.Save();
                            var requesttoreturn = _mapper.Map<CompaniesRequestsDto>(requestEntity);
                            return Ok(requesttoreturn);
                        }
                    _logger.LogError("Entered Request Details are Invalid");
                    return BadRequest();
                }
            }
            return BadRequest(companiesRequestsDto);
        }

        [HttpPatch("{mobile}")]
        public IActionResult PartiallyUpdateCompany(string mobile,
[FromBody] JsonPatchDocument<CompanyUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var companyEntity = _repository.company.GetCompanyFromMobile(mobile, trackChanges:
           true);
            if (companyEntity == null)
            {
                _logger.LogInfo($"Company with mobile: {mobile} doesn't exist in the database.");
                return NotFound();
            }
            var companyToPatch = _mapper.Map<CompanyUpdateDto>(companyEntity);
            patchDoc.ApplyTo(companyToPatch);
            _mapper.Map(companyToPatch, companyEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
