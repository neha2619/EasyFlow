using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Util
{
    public class Utilities : IUtil
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly CompanyReq _companyReq;

        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        private string sender;
        private string senderusername;

        public Utilities(ILoggerManager logger, CompanyReq companyReq, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
            _companyReq = companyReq;
            sender = "chandrawansheakv@gmail.com";
            senderusername = "EasyFlow Subsystems";
        }

        public int GenerateOtp()
        {
            Random rand = new Random();
            return rand.Next(1000, 9999);
        }

        public bool GetTimeDifference(string timestamp)
        {

            DateTime dateFromString = DateTime.Parse(timestamp);
            TimeSpan spn = DateTime.Now - dateFromString;

            if (spn.Minutes > 1 && spn.Seconds > 20)
            {

                return false;
            }
            else
                return true;
        }



        public void SendEmail(string to, string msgbody, string msgsub)
        {
            MailAddress from = new MailAddress(sender, senderusername);
            MailAddress reciever = new MailAddress(to);
            MailMessage mailMessage = new MailMessage(from, reciever);
            mailMessage.Subject = msgsub;
            mailMessage.Body = msgbody;
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "chandrawansheakv@gmail.com",
                Password = "humptiaejlusaiiw"
            };
            smtpClient.EnableSsl = true;
            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }


        public bool NotifyForKYC(string KYCstatus)
        {
            if (KYCstatus != "Yes")
            {
                return true;
            }
            return false;
        }
        public bool CheckForFirstLogin(Guid id)
        {
            int totalSignin = _repository.Timestamps.GetTotalLogins(id, trackChanges: false);
            if (totalSignin > 0)
            {
                return false;
            }
            return true;
        }
    }
}
