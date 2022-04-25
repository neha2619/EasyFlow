using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFlow.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private ILoggerManager _logger;
        public WeatherForecastController(IRepositoryManager repository, ILoggerManager logger)
        {
            _logger = logger;
            _repository = repository;
        }
      
 [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.LogInfo("Here is info message from our values controller.");
            _logger.LogDebug("Here is debug message from our values controller.");
            _logger.LogWarn("Here is warn message from our values controller.");
            _logger.LogError("Here is an error message from our values controller.");
        
       
         //   _repository.company.AnyMethodFromCompanyRepository();
           // _repository.Worker.AnyMethodFromWorkerRepository();
           // _repository.Admin.AnyMethodFromAdminRepository();
           // _repository.AdminWorker.AnyMethodFromAdminWorkerRepository();
           // _repository.AdminCompany.AnyMethodFromAdminCompanyRepository();

            return new string[] { "value1", "value2" };
        }

    }
}
