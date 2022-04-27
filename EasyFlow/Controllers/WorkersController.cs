using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public IActionResult RegisterWorker(WorkerForRegistrationDto worker)
        {
           if (worker == null)
            {
                _logger.LogInfo("Worker Registration Object is Null");
                return BadRequest("The Sent Worker Registration Object is Null");
            }
           var workerEntity = _mapper.Map<Worker>(worker);
            _repository.Worker.Create(workerEntity);
            _repository.Save();
            var workerToReturn = _mapper.Map<WorkerDto>(workerEntity);
            return CreatedAtRoute("workerById", new { id = workerToReturn.Id },workerToReturn);

    }
    }
  
   
}
