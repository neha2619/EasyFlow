using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

//using System.Threading;

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
        private readonly AdminUpdateDto _adminUpdate;
        private readonly Timestamps _timestamps;
        private readonly WorkerReq _workerReq;
        private readonly TotalCounts _totalCounts;
        private readonly TotalCounts _totalCounts1;



        private static int GeneratedOtp = 0;
        private static int count = 0;
        private static bool otpMatched = false;
        private static DateTime lastLoginTime = DateTime.MinValue;

        public AdminController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IGlobalValidationUtil validate, IUtil utilities, OTPs otp, CompanyReq companyReq, AdminCompany adminCompany, DashBoardDto dashboardDto, AdminUpdateDto adminUpdate, Timestamps timestamps, WorkerReq workerReq, TotalCounts totalCounts, TotalCounts totalCounts1)
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
            _adminUpdate = adminUpdate;
            _timestamps = timestamps;
            _workerReq = workerReq;
            _totalCounts = totalCounts;
            _totalCounts1 = totalCounts1;


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
            bool IsFirstLogin;
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
                            IsFirstLogin = _utilities.CheckForFirstLogin(Admin.Id);
                            if (IsFirstLogin)
                            {
                                _timestamps.RecipientID = Admin.Id;
                                _timestamps.TimeStamp = lastLoginTime.ToString();
                                _repository.Timestamps.InsertTimestamp(_timestamps);
                                _repository.Save();
                            }
                            var x = CheckNotificationsFromCompany(Admin.Id);
                            lastLoginTime = DateTime.Now;
                            _timestamps.id = new Guid();
                            _timestamps.RecipientID = Admin.Id;
                            _timestamps.TimeStamp = lastLoginTime.ToString();
                            _repository.Timestamps.InsertTimestamp(_timestamps);
                            _repository.Save();
                            return x;
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
                            IsFirstLogin = _utilities.CheckForFirstLogin(Admin.Id);
                            if (IsFirstLogin)
                            {
                                _timestamps.RecipientID = Admin.Id;
                                _timestamps.TimeStamp = lastLoginTime.ToString();
                                _repository.Timestamps.InsertTimestamp(_timestamps);
                                _repository.Save();
                            }
                            var x = CheckNotificationsFromCompany(Admin.Id);

                            lastLoginTime = DateTime.Now;
                            _timestamps.id = new Guid();
                            _timestamps.RecipientID = Admin.Id;
                            _timestamps.TimeStamp = lastLoginTime.ToString();
                            _repository.Timestamps.InsertTimestamp(_timestamps);
                            _repository.Save();
                            return x;

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

        [HttpGet("postrequest/company")]
        public IActionResult PostRequestsToCompany()
        {
            int c = 0;
            bool flag = true;
            DateTime mindateTime = DateTime.MaxValue;


            var allworkFound = _repository.AdminCompany.GetAllRequest(trackChanges: false);
            var allworkerFound = _repository.AdminWorker.GetAllRequest(trackChanges: false);
            if (allworkFound != null && allworkerFound != null)
            {

                _logger.LogDebug($"this is match voudtn: {c}");

                foreach (var work in allworkFound)
                {
                    _logger.LogInfo($"this is company id {work.CompanyId}");
                    _logger.LogInfo($"this is company namne {work.CompanyName}");
                    _logger.LogInfo($"this is name : {work.CompanyName} and its vacancy is {work.Vacancy}");

                    foreach (var worker in allworkerFound)
                    {
                        var workerprofile = _repository.Worker.GetWorkerFromID(worker.WorkerId, trackChanges: false);

                        int vacancy = Convert.ToInt32(work.Vacancy);
                        int totalworkers = Convert.ToInt32(worker.CreatedOn.Count());
                        _logger.LogDebug($"count is :{totalworkers} vcount is {vacancy}");

                        if (String.Equals(work.WorkerType, worker.WorkerType, StringComparison.OrdinalIgnoreCase))
                        {
                            var recommendedworkerlist = _repository.CompanyReq.GetAllSuggestedWorkers(work.CompanyId, trackChanges: false);
                            if (recommendedworkerlist != null)
                                foreach (var recommendedworker in recommendedworkerlist)
                                {
                                    if (recommendedworker.WorkerId.Equals(worker.WorkerId.ToString()) && recommendedworker.CompanyId.Equals(work.CompanyId))
                                    {
                                        flag = false;
                                        _logger.LogDebug($"here we got false");
                                    }
                                }
                            if (flag)
                            {
                                _logger.LogInfo($"company name is : {work.CompanyName} worker name is : {worker.WorkerName}");
                                _companyReq.Id = new Guid();
                                _companyReq.WorkerId = worker.WorkerId.ToString();
                                _companyReq.CompanyId = work.CompanyId.ToString();
                                _companyReq.WorkerName = worker.WorkerName;
                                _companyReq.WorkerType = worker.WorkerType;
                                _companyReq.email = workerprofile.WorkerMail;
                                _companyReq.Mobile = workerprofile.WorkerMobile;
                                _companyReq.Location = worker.Location;
                                _companyReq.RequestStatus = "Pending";
                                _companyReq.CreatedOn = DateTime.Now.ToString();
                                _repository.CompanyReq.CreateCompanyRequest(_companyReq);
                                _repository.Save();
                                vacancy--;
                                _logger.LogInfo($"vacancy left : {vacancy}");
                            }
                            else
                            {
                                _logger.LogInfo($"we got a same worker");
                                flag = true;
                            }
                        }
                        else
                        {
                            _logger.LogDebug($" else company name {work.CompanyName} worker name is : {worker.WorkerName}");
                        }

                    }

                }
                return Ok("Request Succesfully Sent");
            }

            return BadRequest();




        }
        [HttpGet("postrequest/worker")]
        public IActionResult PostRequestsToWorker()
        {
            int c = 0;
            bool flag = true;
            int vacancyToReturn = 0;
            DateTime mindateTime = DateTime.MaxValue;
            List<String> createdon = new List<string>();
            List<String> createdonForCompany = new List<string>();



            var allworkFound = _repository.AdminCompany.GetAllRequest(trackChanges: false);
            var allworkerFound = _repository.AdminWorker.GetAllRequest(trackChanges: false);
            if (allworkFound != null && allworkerFound != null)
            {

                _logger.LogDebug($"this is match voudtn: {c}");

                foreach (var worker in allworkerFound)
                {
                    _logger.LogInfo($"this is company id {worker.WorkerName}");

                    foreach (var work in allworkFound)
                    {
                        var companyprofile = _repository.company.GetCompanyFromId(work.CompanyId, trackChanges: false);

                        int vacancy = Convert.ToInt32(work.Vacancy);
                        int totalworkers = Convert.ToInt32(worker.CreatedOn.Count());
                        _logger.LogDebug($"count is :{totalworkers} vcount is {vacancy}");

                        if (String.Equals(work.WorkerType, worker.WorkerType, StringComparison.OrdinalIgnoreCase))
                        {
                            var recommendedworkerlist = _repository.CompanyReq.GetAllSuggestedWorkers(work.CompanyId, trackChanges: false);
                            if (recommendedworkerlist != null)
                                foreach (var recommendedworker in recommendedworkerlist)
                                {
                                    if (recommendedworker.WorkerId.Equals(worker.WorkerId.ToString()) && recommendedworker.CompanyId.Equals(work.CompanyId))
                                    {
                                        flag = false;
                                        _logger.LogDebug($"here we got false");
                                    }
                                }
                            if (flag)
                            {
                                _logger.LogInfo($"company name is : {work.CompanyName} worker name is : {worker.WorkerName}");
                                _workerReq.Id = new Guid();
                                _workerReq.WorkerId = worker.WorkerId.ToString();
                                _workerReq.CompanyId = work.CompanyId.ToString();
                                _workerReq.CompanyName = work.CompanyName;
                                _workerReq.WorkerType = worker.WorkerType;
                                _workerReq.CompanyMail = companyprofile.CompanyMail;
                                _workerReq.CompanyMobile = companyprofile.CompanyMobile;
                                _workerReq.Location = worker.Location;
                                _workerReq.RequestStatus = "Pending";
                                _workerReq.CreatedOn = DateTime.Now.ToString();
                                _repository.WorkerReq.CreateWorkerRequest(_workerReq);
                                _repository.Save();
                                vacancy--;
                                _logger.LogInfo($"vacancy left : {vacancy}");
                            }
                            else
                            {
                                _logger.LogInfo($"we got a same worker");
                                flag = true;
                            }
                        }
                        else
                        {
                            _logger.LogDebug($" else company name {work.CompanyName} worker name is : {worker.WorkerName}");
                        }

                    }

                }

            }

            return Ok("Request Succesfully Sent");



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
        [HttpGet("changepassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (otpMatched)
            {
                //UPDATE THE PASSWORD HERE
                var IsPasswordUpdated = UpdatePassword(changePasswordDto);
                return IsPasswordUpdated;

            }
            return BadRequest();
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {

            int[] monthsForWorker = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] monthsForCompany = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //_dashboardDto.totalcounts = _repository.TotalCounts.GetAllCounts(trackChanges: false);

            _dashboardDto.worker = _repository.Worker.GetTopRatedWorker(trackChanges: false);
            var workers = _repository.Worker.GetAllWorkers(trackChanges: false);
            var companies = _repository.company.GetCompaniesByCreatedOn(trackChanges: false);


            foreach (var worker in workers)
            {
                int m = DateTime.Parse(worker.CreatedOn).Month - 1;
                monthsForWorker[m]++;
            }
            _dashboardDto.workerbyMonth = monthsForWorker;
            foreach (var company in companies)
            {
                int m = DateTime.Parse(company.CreatedOn).Month - 1;
                monthsForCompany[m]++;
            }
            _dashboardDto.CompanybyMonth = monthsForCompany;
            return Ok(_dashboardDto);
        }



        [HttpPut]
        public IActionResult UpdatePassword(ChangePasswordDto changePassword)
        {

            var adminProfile = _repository.Admin.GetAdminPasswordFromEmail(changePassword.recipientMail, trackChanges: false);
            _repository.Admin.Delete(_repository.Admin.GetAdminPasswordFromEmail(changePassword.recipientMail, trackChanges: false));
            if (changePassword.password.Equals(changePassword.confirmPassword))
            {
                if (!(_validate.IsPasswdStrong(changePassword.confirmPassword)))
                {
                    _logger.LogError("Password is too weak");
                    return BadRequest("Password is too weak");
                }
                _adminUpdate.Name = adminProfile.Name;
                _adminUpdate.Email = adminProfile.Email;
                _adminUpdate.Pass = changePassword.confirmPassword;
                _adminUpdate.Mobile = adminProfile.Mobile;
                var adminUpdateEntity = _mapper.Map<Admin>(_adminUpdate);
                _repository.Admin.Update(adminUpdateEntity);
                _repository.Save();

                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("Notifications/Company/{id}")]
        public IActionResult CheckNotificationsFromCompany(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogInfo("ID is empty");
                return BadRequest();
            }
            var requestsofWorkers = _repository.AdminWorker.GetAllRequest(trackChanges: false);

            var requests = _repository.AdminCompany.GetAllRequest(trackChanges: false);
            _logger.LogDebug($"this is req{requests.ToString()}");
            var loginTimestamps = _repository.Timestamps.GetLastLoginTimeById(id, trackchanges: false).Reverse();
            foreach (var ts in loginTimestamps)
            {
                _logger.LogInfo($"this is ts :{ts.TimeStamp}");
            }
            lastLoginTime = DateTime.Parse(loginTimestamps.ElementAt(0).TimeStamp);

            List<AdminCompany> LatestReq = new List<AdminCompany>();
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
            //foreach (int i = 0 ; int<>) {
            //_logger.LogDebug($"{x}thi s is latest req"); }

            if (LatestReq.Count > 0)
            {
                for (int i = 0; i < LatestReq.Count; i++)
                {
                    var newRequests = x[i];
                }
                return Ok(requests);
            }
            return NoContent();
        }
        [HttpGet("Notification/worker/{id}")]
        public IActionResult CheckNotificationsFromWorker(Guid id)
        {
            if (id == Guid.Empty)
            {
                _logger.LogInfo("ID is empty");
                return BadRequest();
            }
            var requestsofWorkers = _repository.AdminWorker.GetAllRequest(trackChanges: false);
            var loginTimestamps = _repository.Timestamps.GetLastLoginTimeById(id, trackchanges: false).Reverse();
            lastLoginTime = DateTime.Parse(loginTimestamps.ElementAt(0).TimeStamp);
            List<AdminWorker> LatestReqofWorkers = new List<AdminWorker>();
            foreach (var request in requestsofWorkers)
            {
                if (DateTime.Parse(request.CreatedOn) > lastLoginTime)
                {
                    count++;
                    LatestReqofWorkers.Add(request);

                }
                else
                {
                    continue;
                }
            }
            var x = LatestReqofWorkers.ToArray();

            if (LatestReqofWorkers.Count > 0)
            {
                for (int i = 0; i < LatestReqofWorkers.Count; i++)
                {
                    var newRequestsofWorkers = x[i];
                }
                return Ok(LatestReqofWorkers);
            }
            return NoContent();
        }
    }
}
