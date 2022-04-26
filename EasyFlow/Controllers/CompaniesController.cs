using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
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
        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper
            mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _repository.company.GetAllCompanies(trackChanges: false);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            
            return Ok(companiesDto);

        }
        [HttpGet("{Id}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repository.company.GetCompany(id,trackChanges:false);
            if (company == null)
            {
                _logger.LogInfo($" Company with id {id} doesn't exist in the Database");
                return NotFound();
            }
            var companyDto = _mapper.Map<CompanyDto>(company);
            return Ok(companyDto);
        }


    }
}
