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
        private readonly IUtil _utilities;
        private readonly OTPs _otp;
        private readonly Timestamps _timestamps;
        private readonly company _company;

        private static int GeneratedOtp = 0;
        private static bool otpMatched = false;
        private static string companyId = "";
        private static int count = 0;

        private static DateTime lastLoginTime = DateTime.MinValue;

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper
            mapper, IGlobalValidationUtil validate, IUtil utilities, OTPs otp, Timestamps timestamps,company company)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validate = validate;
            _utilities = utilities;
            _otp = otp;
            _timestamps = timestamps;
            _company = company;

        }
        [HttpGet(Name = "companyById")]
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
            if (companyCreation.CompanyName != "" && companyCreation.CompanyName != "" && companyCreation.CompanyMail != "" && companyCreation.CompanyPass != "" && companyCreation.CompanyMobile != "")
            {

                if (!(_validate.IsEmailValid(companyCreation.CompanyMail)))
                {
                    _logger.LogInfo("Email is invalid ");
                    return BadRequest("Invalid Email");
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
                companyEntity.CreatedOn = DateTime.Now.ToString();
                _repository.company.CreateCompany(companyEntity);
                _repository.Save();
                var companyToReturn = _mapper.Map<CompanyRegistrationDto>(companyEntity);
                return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id },
               companyToReturn);
            }
            _logger.LogError("Fields can not be null in Company Registeration");
            return BadRequest("Fields can not be null in Company Registeration");

        }

        [HttpPost("login")]
        public IActionResult LoginCompanyByEmail([FromBody] LoginDto companyLogin)
        {
            _logger.LogInfo($"this is email :{companyLogin.Email} pass is {companyLogin.Pass}");
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
                    companyId =  company.Id.ToString();
                    if (company != null)
                    {
                        if (companyLogin.Pass.Equals(company.CompanyPass))
                        {
                            bool isFirstLogin = _utilities.CheckForFirstLogin(company.Id);
                            if (isFirstLogin)
                            {
                                _timestamps.RecipientID = company.Id;
                                _timestamps.TimeStamp = lastLoginTime.ToString();
                                _repository.Timestamps.InsertTimestamp(_timestamps);
                                _repository.Save();
                            }
                            var x = CheckNotifications(company.Id);
                            lastLoginTime = DateTime.Now;
                            _timestamps.id = new Guid();
                            _timestamps.RecipientID = company.Id;
                            _timestamps.TimeStamp = lastLoginTime.ToString();
                            _repository.Timestamps.InsertTimestamp(_timestamps);
                            _repository.Save();
                            return x;
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
                   
                    if(company.Id.ToString() != null)
                    companyId = company.Id.ToString();


                    if (company != null)
                    {
                        if (companyLogin.Pass.Equals(company.CompanyPass))
                        {
                            bool isFirstLogin = _utilities.CheckForFirstLogin(company.Id);
                            if (isFirstLogin)
                            {
                                _timestamps.RecipientID = company.Id;
                                _timestamps.TimeStamp = lastLoginTime.ToString();
                                _repository.Timestamps.InsertTimestamp(_timestamps);
                                _repository.Save();
                            }
                            var x = CheckNotifications(company.Id);
                            lastLoginTime = DateTime.Now;
                            _timestamps.id = new Guid();
                            _timestamps.RecipientID = company.Id;
                            _timestamps.TimeStamp = lastLoginTime.ToString();
                            _repository.Timestamps.InsertTimestamp(_timestamps);
                            _repository.Save();
                            return x;
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

        [HttpPost("request")]
        public IActionResult RequestAdminForWorker( CompaniesRequestsDto companiesRequestsDto)
        {
            if (companiesRequestsDto.WorkerType != null || companiesRequestsDto.Location != null || companiesRequestsDto.Vacancy != null)
            {
                var company = _repository.company.GetCompanyFromId(Guid.Parse(companyId), trackChanges: false);

                if (company != null)
                {
                    if (_validate.IsNumberValid(companiesRequestsDto.Vacancy) && _validate.IsStringValid(companiesRequestsDto.WorkerType))
                        if (Convert.ToInt64(companiesRequestsDto.Vacancy) > 0)
                        {
                            _logger.LogDebug($"this is companyname {company.CompanyName}");
                            companiesRequestsDto.CompanyName = company.CompanyName;
                            companiesRequestsDto.CompanyID = Guid.Parse(companyId);
                            
                            companiesRequestsDto.RequestState = "Request is Processing";
                            companiesRequestsDto.CreatedOn = DateTime.Now.ToString();
                            var requestEntity = _mapper.Map<AdminCompany>(companiesRequestsDto);
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
        [HttpGet("checkrequeststatus")]
        public IActionResult CheckRequestStatus(CheckRequestsDto checkRequestsDto)
        {
            if (checkRequestsDto != null)
            {

                if (checkRequestsDto.WorkerType != "")
                {
                    if (_validate.IsStringValid(checkRequestsDto.WorkerType.ToString()))
                    {
                        var requests = _repository.AdminCompany.GetRequestsForWorkerTypeByCompanyId(checkRequestsDto.userID, checkRequestsDto.WorkerType, trackChanges: false);

                        if (requests != null)
                        {
                            var RequestsDto = requests.Select(c => new ReturnRequestStatusToCompanyDto
                            {
                                WorkerType = c.WorkerType,
                                Location = c.Location,
                                Vacancy = c.Vacancy,
                                RequestState = c.RequestState,
                                CreatedOn = c.CreatedOn
                            }).ToList();

                            if (RequestsDto.Count > 0)
                            {
                                return Ok(RequestsDto);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }

                    return BadRequest();
                }
                else
                {

                    if (checkRequestsDto.WorkerType == "")
                    {

                        var requests = _repository.AdminCompany.GetRequestsForWorkerTypeByCompanyId(checkRequestsDto.userID, checkRequestsDto.WorkerType, trackChanges: false);

                        var RequestsDto = requests.Select(c => new ReturnRequestStatusToCompanyDto
                        {
                            WorkerType = c.WorkerType,
                            Location = c.Location,
                            Vacancy = c.Vacancy,
                            RequestState = c.RequestState,
                            CreatedOn = c.CreatedOn
                        }).ToList();

                        _logger.LogInfo(RequestsDto.ToString());
                        return Ok(RequestsDto);
                    }
                }
            }
            return BadRequest("CheckRequestDto is Null");
        }

        [HttpPatch("update")]
        public IActionResult PartiallyUpdateCompany(CompanyUpdateDto updateProfile)
        {
            
            var companyProfile=_repository.company.GetCompanyFromId(Guid.Parse(companyId),trackChanges: false);
            if (companyProfile == null)
            {
                _logger.LogError("company not found");
                return BadRequest("company Not Found");
            }

            if (!string.Equals(companyProfile.CompanyName, updateProfile.CompanyName, StringComparison.OrdinalIgnoreCase))
            {
                companyProfile.CompanyName = updateProfile.CompanyName;
            }

            if (!string.Equals(companyProfile.CompanyCin, updateProfile.CompanyCin, StringComparison.OrdinalIgnoreCase))
            {
                companyProfile.CompanyCin = updateProfile.CompanyCin;
            } 
            if (!string.Equals(companyProfile.CompanyGstin, updateProfile.CompanyGstin, StringComparison.OrdinalIgnoreCase))
            {
                companyProfile.CompanyGstin = updateProfile.CompanyGstin;
            }

            if (!string.Equals(companyProfile.CompanyMobile, updateProfile.CompanyMobile, StringComparison.OrdinalIgnoreCase))
            {
                companyProfile.CompanyMobile = updateProfile.CompanyMobile;
            }

           

            companyProfile.UpdatedOn  = DateTime.Now.ToString();
            _repository.company.UpdateCompany(companyProfile);
            var companyToReturn = _mapper.Map<CompanyDto>(companyProfile);
            _repository.Save();
            return Ok("Company Updated Succesfully");
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
        public IActionResult UpdatePassword(ChangePasswordDto changePassword)
        {
            var companyProfile = _repository.company.GetCompanyFromId(Guid.Parse(companyId), trackChanges: false);
            if (companyProfile == null)
            {
                _logger.LogError("company not found");
                return BadRequest("company Not Found");
            }
            if (otpMatched)
            {
                if (changePassword.password.Equals(changePassword.confirmPassword))
                {
                    companyProfile.CompanyPass = changePassword.confirmPassword;
                    companyProfile.UpdatedOn = DateTime.Now.ToString();
                    _repository.company.UpdateCompany(companyProfile);
                    var companyToReturn = _mapper.Map<CompanyDto>(companyProfile);
                    _repository.Save();
                    return Ok("Password Changed Succesfully");
                }
                return BadRequest("Confirm Password not matched");
            }
            else
            {
                return BadRequest("OTP is Incorrect");
            }

        }

        [HttpGet("Notifications")]
        public IActionResult CheckNotifications(Guid id)
        {
            var requests = _repository.CompanyReq.GetAllSuggestedWorkers(id, trackChanges: false);
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
                    var newRequests = x[i];
                }
                return Ok(LatestReq);
            }
            return NoContent();
        }

        [HttpGet("viewrequests")]
        public IActionResult ViewRequestsOfWorkers()
        {
            var requests = _repository.CompanyReq.GetAllSuggestedWorkersByCompanyID(companyId, trackChanges: false);
            var RequestToReturn = _mapper.Map<IEnumerable<CompanyViewRequestsDto>>(requests);
            var x = RequestToReturn.ToArray();



            for (int i = 0; i < x.Length; i++)
            {
                x[i].Serial = i + 1;
            }
            return Ok(x.ToList());
        }
        [HttpPost("updatecompany")]
        public IActionResult P(CompanyUpdateDto updateProfile)
        {
            return PartiallyUpdateCompany(updateProfile);
        } 
        [HttpPost("updatecompanypass")]
        public IActionResult P(ChangePasswordDto changePassword)
        {
            return UpdatePassword(changePassword);
        }

    }
}
