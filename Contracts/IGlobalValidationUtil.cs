using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGlobalValidationUtil
    {
        bool IsEmailValid(string email);
        bool IsMobileValid(string mob);
        bool IsGstinValid(string gstin);
        bool IsCinValid(string cin);
        bool IsAadhaarValid(string aadhaar);
        bool IsPasswdStrong(string passwd);

    }
}
