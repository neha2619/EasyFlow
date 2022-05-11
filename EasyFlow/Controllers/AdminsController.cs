using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

//using System.Threading;
using System.Timers;

namespace EasyFlow.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IGlobalValidationUtil _validate;
        private readonly IUtil _utilities;
        private readonly CompanyReq _companyReq;
        private readonly AdminCompany _adminCompany;
        private readonly DashBoardDto _dashboardDto;
        private readonly OTPs _otp;
        private static bool otpMatched = false;


        private static int GeneratedOtp = 0;

        public AdminController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IGlobalValidationUtil validate, IUtil utilities, OTPs otp, CompanyReq companyReq, AdminCompany adminCompany, DashBoardDto dashboardDto)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validate = validate;
            _utilities = utilities;
            _otp = otp;
            _companyReq = companyReq;
            _adminCompany = adminCompany;
            _dashboardDto = dashboardDto;

        }
        [HttpGet(Name = "AdminById")]
        public IActionResult GetAdmin()
        {

            var admin = _repository.Admin.GetAllAdmin(trackChanges: false);
            var adminDto = _mapper.Map<IEnumerable<AdminDto>>(admin);

            return Ok(adminDto);

        }
        [HttpPost("register")]
        public IActionResult RegisterAdmin([FromBody] AdminForRegistrationDto admin)
        {

            if (admin == null)
            {
                _logger.LogError("AdminForRegistration object sent from the client is null");
                return BadRequest("AdminForRegistration Object is Null");
            }
            if (!(_validate.IsEmailValid(admin.Email)))
            {
                _logger.LogInfo("Email is invalid ");
                return BadRequest("Invalid Email");
            }
            if (!(_validate.IsMobileValid(admin.Mobile)))
            {
                _logger.LogError("Company Mobile Number is Not Valid");
                return BadRequest("Invalid Mobile");
            }
            if (!(_validate.IsPasswdStrong(admin.Pass)))
            {
                _logger.LogError("Password is too weak");
                return BadRequest("Password is too weak");
            }
            var adminEntity = _mapper.Map<Admin>(admin);
            _repository.Admin.CreateAdmin(adminEntity);
            _repository.Save();
            var adminToReturn = _mapper.Map<AdminDto>(adminEntity);
            return CreatedAtRoute("AdminById", new { id = adminToReturn.Id },
           adminToReturn);
        }
        [HttpGet("login")]
        public IActionResult LoginAdmin([FromBody] LoginDto adminLogin)
        {
            if (adminLogin.Email != null && adminLogin.Mobile != null && adminLogin.Pass != null)
            {
                return StatusCode(405, "Now Allowed");
            }
            if (adminLogin.Mobile != null)
            {
                if (!(_validate.IsMobileValid(adminLogin.Mobile)))
                {
                    _logger.LogError("Company Mobile Number is Not Valid");
                    return BadRequest("Invalid Mobile");
                }
                if (adminLogin.Pass != null)
                {
                    var Admin = _repository.Admin.GetAdminPasswordFromMobile(adminLogin.Mobile, trackChanges: false);

                    if (Admin != null)
                    {
                        if (adminLogin.Pass.Equals(Admin.Pass))
                        {

                            return Ok($"Login Successful");
                        }
                        return BadRequest("Password Incorrect");
                    }
                    _logger.LogInfo($"Admin with Mobile {adminLogin.Mobile} not found");
                    return BadRequest($"Admin with Mobile {adminLogin.Mobile} not found");
                }
                _logger.LogError("Fields can not be null");
                return BadRequest("Fields can not be null");
            }
            else
            {
                if (adminLogin.Email != null && adminLogin.Pass != null)
                {
                    if (!(_validate.IsEmailValid(adminLogin.Email)))
                    {
                        _logger.LogInfo("Email is invalid ");
                        return BadRequest("Invalid Email");
                    }
                    var Admin = _repository.Admin.GetAdminPasswordFromEmail(adminLogin.Email, trackChanges: false);

                    if (Admin != null)
                    {
                        if (adminLogin.Pass.Equals(Admin.Pass))
                        {
                            return Ok($"Login Successful");
                            //call here the notify functions

                        }
                        return BadRequest("Password Incorrect");

                    }
                    _logger.LogInfo($"Admin with email {adminLogin.Email} not found");
                    return BadRequest($"Admin with email {adminLogin.Email} not found");
                }
            }
            _logger.LogError("Fields can not be null");
            return BadRequest("Fields can not be null");

        }
        [HttpPatch("{mobile}")]
        public IActionResult PartiallyUpdateAdmin(string mobile,
[FromBody] JsonPatchDocument<AdminUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var adminEntity = _repository.Admin.GetAdminFromMobile(mobile, trackChanges:
           true);
            if (adminEntity == null)
            {
                _logger.LogInfo($"Admin with mobile: {mobile} doesn't exist in the database.");
                return NotFound();
            }
            var adminToPatch = _mapper.Map<AdminUpdateDto>(adminEntity);
            patchDoc.ApplyTo(adminToPatch);
            _mapper.Map(adminToPatch, adminEntity);
            _repository.Save();
            return NoContent();
        }

        [HttpPost("postrequest")]
        public IActionResult PostRequestsForWorker(GetRequestDetailsFromCompanyDto getRequest)
        {
            int c = 0;
            if (getRequest.workerType != "" && getRequest.location != "")
            {
                if (_validate.IsStringValid(getRequest.workerType))
                {

                    var workFound = _repository.AdminCompany.GetRequestsByCompanyId(getRequest.CompanyId, trackChanges: false);
                    var workersFound = _repository.AdminWorker.GetAllRequestByWorkerType(getRequest.workerType, trackChanges: false);
                    var workersDto = _mapper.Map<IEnumerable<AdminCompany>>(workFound);
                    foreach (AdminWorker worker in workersFound)
                    {
                        if (worker.WorkerType.Equals(getRequest.workerType) && worker.Location.Equals(getRequest.location))
                        {
                            c++;
                        }

                    }
                    DateTime mindateTime = DateTime.MaxValue;
                    List<String> createdon = new List<string>();
                    foreach (AdminWorker worker in workersFound)
                    {
                        if (worker.WorkerType.Equals(getRequest.workerType) && worker.Location.Equals(getRequest.location) && c == 1)
                        {
                            _companyReq.WorkerId = worker.WorkerId;
                            _companyReq.CompanyId = getRequest.CompanyId;
                            _companyReq.RequestStatus = "Pending";
                            _companyReq.CreatedOn = DateTime.Now.ToString();
                        }
                        else
                        {
                            var we = _repository.AdminWorker.GetAllRequestByCreatedOn(trackChanges:false);
                            if (we != null &&  createdon.Contains(we.CreatedOn))
                            {
                                _logger.LogInfo($" id {we.WorkerId} tme:{we.CreatedOn}");
                                createdon.Add(we.CreatedOn);
                            }
                        }
                    }
                    return Ok(_companyReq);
                }
                _logger.LogInfo("Worker Type is not valid");
                return BadRequest("WorkerType Not Valid");
            }
            _logger.LogError("Entered Request Details are Invalid");
            return BadRequest();
        }
        [HttpGet("sendotp")]
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
        public IActionResult ChangePassword(OtpsDto OtpDto)
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
                    //UPDATE THE PASSWORD HERE
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            int[] monthsForWorker = new int[11] {0,0,0,0,0,0,0,0,0,0,0};
            int[] monthsForCompany = new int[11] {0,0,0,0,0,0,0,0,0,0,0};
            _dashboardDto.totalworkers = _repository.Worker.CountAllWorkers(trackChanges: false) ;
            _dashboardDto.totalcompany = _repository.company.CountAllCompanies(trackChanges: false) ;
            _dashboardDto.worker = _repository.Worker.GetTopRatedWorker(trackChanges: false) ;
            var workers = _repository.Worker.GetAllWorkers(trackChanges: false) ;
           var companies=  _repository.company.GetCompaniesByCreatedOn(trackChanges: false) ;
           foreach (var worker in workers)
            {
                int m = DateTime.Parse(worker.CreatedOn).Month;
                monthsForWorker[m]++;
            }
           foreach (var company in companies)
            {
                int m = DateTime.Parse(company.CreatedOn).Month;
                monthsForCompany[m]++;

            }
            _dashboardDto.CompanybyMonth = monthsForCompany;
            _dashboardDto.workerbyMonth = monthsForWorker;


            return Ok(_dashboardDto);
        }
    }
}
