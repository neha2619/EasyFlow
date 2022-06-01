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
    [Route("api/workers")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IGlobalValidationUtil _validate;
        private readonly IUtil _utilities;
        private readonly OTPs _otp;
        private readonly Timestamps _timestamps;

        private static int GeneratedOtp = 0;
        private static int count = 0;
        private static bool otpMatched = false;
        private static  string workerId = "";
        private static DateTime lastLoginTime = DateTime.MinValue;


        public WorkersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IGlobalValidationUtil validate, IUtil utilities, OTPs otp, Timestamps timestamps)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validate = validate;
            _utilities = utilities;
            _otp = otp;
            _timestamps = timestamps;

        }
        [HttpGet(Name = "WorkerById")]
        public IActionResult GetWorkers()
        {

            var workers = _repository.Worker.GetAllWorkers(trackChanges: false);
            var workersDto = _mapper.Map<IEnumerable<WorkerDto>>(workers);

            return Ok(workersDto);

        }
        [HttpPost("register")]
        public IActionResult RegisterWorker([FromBody] WorkerForRegistrationDto worker)
        {
          
           if (worker != null )
            {

                if (!(_validate.IsEmailValid(worker.WorkerMail)))
                {
                    _logger.LogInfo("Email is invalid ");
                    return BadRequest("Invalid Email");
                }
                if (!(_validate.IsMobileValid(worker.WorkerMobile)))
                {
                    _logger.LogError("Worker Mobile Number is Not Valid");
                    return BadRequest("Invalid Mobile");
                }
                if (!(_validate.IsPasswdStrong(worker.WorkerPass)))
                {

                    _logger.LogError("Password is too weak");
                    return BadRequest("Password is too weak");
                }
                worker.CreatedOn = DateTime.Now.ToString();
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
        [HttpPost("login")]
        public IActionResult LoginWorker([FromBody]LoginDto workerLogin)
        {
            bool isFirstLogin;
            _logger.LogInfo($"email is :{workerLogin.Email} pass is {workerLogin.Pass}");

            if (workerLogin.Email != null && workerLogin.Mobile != null && workerLogin.Pass != null)
            {
                return StatusCode(405, "Now Allowed");
            }
            if (workerLogin.Mobile != null)
            {
                if (!(_validate.IsMobileValid(workerLogin.Mobile)))
                {
                    _logger.LogError("Worker Mobile Number is Not Valid");
                    return BadRequest("Invalid Mobile");
                }
                if (workerLogin.Pass != null)
                {
                   

                    var Worker = _repository.Worker.GetWorkerPasswordFromMobile(workerLogin.Mobile, trackChanges: false);
                    workerId = Worker.Id.ToString();

                    if (Worker != null)
                    {
                        if (workerLogin.Pass.Equals(Worker.WorkerPass))
                        {
                            isFirstLogin = _utilities.CheckForFirstLogin(Worker.Id);
                            if (isFirstLogin)
                            {
                                _timestamps.RecipientID = Worker.Id;
                                _timestamps.TimeStamp = lastLoginTime.ToString();
                                _repository.Timestamps.InsertTimestamp(_timestamps);
                                _repository.Save();
                            }
                            var x = CheckNotifications(Worker.Id);
                            lastLoginTime = DateTime.Now;
                            _timestamps.id = new Guid();
                            _timestamps.RecipientID = Worker.Id;
                            _timestamps.TimeStamp = lastLoginTime.ToString();
                            _repository.Timestamps.InsertTimestamp(_timestamps);
                            _repository.Save();
                            return x;

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
                    workerId = Worker.Id.ToString();
                    
                    if (Worker != null)
                    {
                        if (workerLogin.Pass.Equals(Worker.WorkerPass))
                        {
                            isFirstLogin = _utilities.CheckForFirstLogin(Worker.Id);
                            if (isFirstLogin)
                            {
                                _timestamps.RecipientID = Worker.Id;
                                _timestamps.TimeStamp = lastLoginTime.ToString();
                                _repository.Timestamps.InsertTimestamp(_timestamps);
                                _repository.Save();
                            }
                            var x = CheckNotifications(Worker.Id);
                            lastLoginTime = DateTime.Now;
                            _timestamps.id = new Guid();
                            _timestamps.RecipientID = Worker.Id;
                            _timestamps.TimeStamp = lastLoginTime.ToString();
                            _repository.Timestamps.InsertTimestamp(_timestamps);
                            _repository.Save();
                            return x;

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
        
        [HttpPost("apply")]
        public IActionResult RequestForJob( WorkerRequestToCompanyDto requestDto)
        {
            _logger.LogError($"{workerId} is here");

            if (workerId!=null && (workerId != ""))
            {
                var worker = _repository.Worker.GetWorkerFromId(Guid.Parse(workerId) ,trackChanges: false);
                if (worker != null)
                {
                    _logger.LogInfo($"this is work type :{requestDto.WorkerType} this is loc : {requestDto.Location}");
                    if (!(_validate.IsStringValid(requestDto.WorkerType)) && !(_validate.IsStringValid(requestDto.Location)))
                    {
                        _logger.LogError("Entered Request Details are Invalid");
                        return BadRequest();
                    }
                    requestDto.WorkerName = worker.WorkerName;
                    requestDto.WorkerId = Guid.Parse( workerId);
                    requestDto.requestState = "Request is Processing";
                    requestDto.CreatedOn = DateTime.Now.ToString();
                    var workerRequest = _mapper.Map<AdminWorker>(requestDto);
                    _repository.AdminWorker.CreateRequest(workerRequest);
                    var requestToReturn = _mapper.Map<WorkerRequestToCompanyDto>(workerRequest);
                    _repository.Save();
                    return Ok(requestToReturn);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpGet("checkrequeststatus")]
        public IActionResult CheckRequestStatus(CheckRequestsDto checkRequestsDto)
        {
            if (checkRequestsDto != null)
            {
                if (checkRequestsDto.WorkerType != "")
                {
                    var requests = _repository.AdminWorker.GetRequestsByWorkerId(checkRequestsDto.userID,trackChanges: false);
                        var reqs = requests.Where(c => c.Equals(checkRequestsDto.WorkerType)).ToList();
                    var RequestsDto = requests.Select(c => new ReturnRequestStatusToWorkerDto
                    {
                        WorkerType = c.WorkerType,
                        Location = c.Location,
                        RequestState = c.RequestState,
                        CreatedOn = c.CreatedOn
                    }).ToList();
                    return Ok(RequestsDto);
                }
                
            }
            return NotFound();

        }
         [HttpPost("sendotp")]
        public IActionResult SendOtp(OtpsDto OtpDto)
        {
            if (!_validate.IsEmailValid(OtpDto.email))
            {
                _logger.LogError("Email is not valid");
                return BadRequest("Email is not Valid");
            }
            GeneratedOtp = _utilities.GenerateOtp();
            try
            {
                string sub = "OTP FOR RESET PASSWORD";
                string body = "Hello the OTP to reset your password is :\n" + GeneratedOtp.ToString() + "\n It is valid for 120 seconds";
                _utilities.SendEmail(OtpDto.email, body, sub);

                var otp = _mapper.Map<OTPs>(_otp);

                _otp.recipientEmail = OtpDto.email;
                _otp.timestamp = DateTime.Now.ToString();

                _repository.oTPs.CreateOtpObject(_otp);
                _repository.Save();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
            return Ok();
        }
        
        [HttpPost("verifyotp")]
        public IActionResult VerifyOtp(OtpsDto OtpDto)
        {
            if (!_validate.IsEmailValid(OtpDto.email))
            {
                _logger.LogError("Email not Valid");
                return BadRequest();
            }
            var Otps = _repository.oTPs.GetOTPTimestampFromEmail(OtpDto.email, trackChanges: false);

            if (_utilities.GetTimeDifference(Otps.timestamp))
            {
                if (GeneratedOtp.ToString().Equals(OtpDto.otp))
                {
                    otpMatched = true;
                    return Ok("Otp matched");

                }
                return BadRequest("Otp Not Matched");
            }
            return BadRequest("Otp Expired");
        }

        [HttpPatch("changepassword")]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {

            if (otpMatched)
            {
                if (changePasswordDto.password.Equals(changePasswordDto.confirmPassword))
                {
                    if (!(_validate.IsPasswdStrong(changePasswordDto.confirmPassword)))
                    {

                        _logger.LogError("Password is too weak");
                        return BadRequest("Password is too weak");
                    }
                    var workerProfile = _repository.Worker.GetWorkerPasswordFromEmail(changePasswordDto.recipientMail,trackChanges: false);
                    workerProfile.WorkerPass = changePasswordDto.confirmPassword;
                    workerProfile.UpdatedOn = DateTime.Now.ToString();
                    _repository.Worker.Update(workerProfile);
                    var workerToReturn=_mapper.Map<WorkerDto>(workerProfile);
                    _repository.Save();
                    return Ok(workerToReturn);
                }
            }
            return BadRequest();

        }
        [HttpGet("Notifications")]
        public IActionResult CheckNotifications(Guid id)
        {
            var requests = _repository.CompanyReq.GetAllSuggestedCompany(id, trackChanges: false);
            var loginTimestamps = _repository.Timestamps.GetLastLoginTimeById(id, trackchanges: false).Reverse();

            lastLoginTime = DateTime.Parse(loginTimestamps.ElementAt(0).TimeStamp);
            List<CompanyReq> LatestReq = new List<CompanyReq>();
            foreach (var request in requests)
            {
                if (DateTime.Parse(request.CreatedOn) > lastLoginTime)
                {
                    count++;
                    LatestReq.Add(request);
                }
                else
                {
                    continue;
                }
            }
            var x = LatestReq.ToArray();

            if (LatestReq.Count > 0)
            {
                for (int i = 0; i < LatestReq.Count; i++)
                {
                    _logger.LogInfo(LatestReq[i].CreatedOn);
                    var newRequests = x[i];
                }
                return Ok(LatestReq);
            }
            return NoContent();
        }
        [HttpGet("viewrequests")]
        public IActionResult ViewRequestsOfCompanies()
        {
            var requests = _repository.WorkerReq.GetWorkerRequestsByWorkerId(workerId,trackChanges: false);
            var RequestToReturn = _mapper.Map < IEnumerable < WorkerViewRequestDto >>(requests);
            var x = RequestToReturn.ToArray();
            for (int i = 0; i < x.Length; i++)
            {
                x[i].Serial = i + 1;
            }
            return Ok(x.ToList());
        }
    }
}
