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
        private static bool otpMatched = false;
        OTPs oTPs;


        private static int  GeneratedOtp = 0;

        public AdminController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IGlobalValidationUtil validate, IUtil utilities)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validate = validate;
            _utilities = utilities;
            oTPs = new OTPs();


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

        [HttpPost("postrequest/{type}")]
        public IActionResult PostRequestsForWorker(string type)
        {

            if (type != null && !(_validate.IsStringValid(type)))
            {
                var workersFound = _repository.AdminCompany.GetAllRequest(type, trackChanges:false);
                var workersDto = _mapper.Map<IEnumerable<WorkerDto>>(workersFound);
                foreach(WorkerDto worr in workersDto)
                {
                    _logger.LogInfo(worr.Id.ToString());
                }
                return Ok(workersFound); 

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
                var otp = _mapper.Map<OTPs>(oTPs);
                otp.recipientEmail = OtpDto.email;
               otp.timestamp = DateTime.Now.ToString();
                _logger.LogInfo(otp.timestamp);

               _repository.oTPs.CreateOtpObject(otp);
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
        public IActionResult ChangePassword( ChangePasswordDto changePasswordDto)
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
    }
}
