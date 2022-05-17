using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUtil
    {
        int GenerateOtp();
        void SendEmail( string to, string msgbody, string msgsub);
        bool GetTimeDifference(string timestamp);
        bool NotifyForKYC(string KYCstatus);
        bool CheckForFirstLogin(Guid id);
    }
}
