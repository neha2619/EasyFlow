using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EasyFlow.Controllers
{
    [Route("api/workers")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public WorkersController(IRepositoryManager repository,ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet(Name = "WorkerById")]
        public IActionResult GetWorkers()
        {

            var workers = _repository.Worker.GetAllWorkers(trackChanges: false);
            var workersDto = _mapper.Map<IEnumerable<WorkerDto>>(workers);

            return Ok(workersDto);

        }

        [HttpPost("register")]
        public IActionResult RegisterWorker(WorkerForRegistrationDto worker)
        {
          
           if (worker != null )
            {
                var workerEntity = _mapper.Map<Worker>(worker);
                _repository.Worker.Create(workerEntity);
                _repository.Save();
                var workerToReturn = _mapper.Map<WorkerDto>(workerEntity);
                _logger.LogInfo(workerToReturn.Id.ToString());
                return CreatedAtRoute("workerById", new { id = workerToReturn.Id }, workerToReturn);
                
            }
            _logger.LogInfo("Worker Registration Object is Null");
            return BadRequest("The Sent Worker Registration Object is Null");
        }
        [HttpGet("login")]
        public IActionResult LoginWorker([FromBody]LoginDto workerLogin)
        {
            if (workerLogin.Email != null && workerLogin.Mobile != null && workerLogin.Pass != null)
            {
                return StatusCode(405, "Now Allowed");
            }
            if (workerLogin.Mobile != null)
            {
                if (workerLogin.Pass != null)
                {
                    var Worker = _repository.Worker.GetWorkerPasswordFromMobile(workerLogin.Mobile, trackChanges: false);

                    if (Worker != null)
                    {
                        if (workerLogin.Pass.Equals(Worker.WorkerPass))
                        {
                            return Ok($"Login Successful");
                        }
                        return BadRequest("Password Incorrect");

                    }
                    _logger.LogInfo($"Worker with Mobile {workerLogin.Mobile} not found");
                    return BadRequest($"Worker with Mobile {workerLogin.Mobile} not found");
                }
                _logger.LogError("Fields can not be null");
                return BadRequest("Fields can not be null");

            }
            else
            {

                if (workerLogin.Email != null && workerLogin.Pass != null)
                {
                    var Worker = _repository.Worker.GetWorkerPasswordFromEmail(workerLogin.Email, trackChanges: false);

                    if (Worker != null)
                    {
                        if (workerLogin.Pass.Equals(Worker.WorkerPass))
                        {
                            return Ok($"Login Successful");
                        }
                        return BadRequest("Password Incorrect");

                    }
                    _logger.LogInfo($"Worker with email {workerLogin.Email} not found");
                    return BadRequest($"Worker with email {workerLogin.Email} not found");
                }
            }
            _logger.LogError("Fields can not be null");
            return BadRequest("Fields can not be null");

        }
        [HttpPatch("{mobile}")]
        public IActionResult PartiallyUpdateWorker( string mobile,
[FromBody] JsonPatchDocument<WorkerUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            
            var workerEntity = _repository.Worker.GetWorkerFromMobile( mobile, trackChanges:
           true);
            if (workerEntity == null)
            {
                _logger.LogInfo($"Worker with mobile: {mobile} doesn't exist in the database.");
                return NotFound();
            }
            var workerToPatch = _mapper.Map<WorkerUpdateDto>(workerEntity);
            patchDoc.ApplyTo(workerToPatch);
            _mapper.Map(workerToPatch, workerEntity);
            _repository.Save();
            return NoContent();
        }

    }
}
