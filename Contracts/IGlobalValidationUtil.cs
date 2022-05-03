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
        bool IsMobilelValid(string mob);
        bool IsGstinlValid(string gstin);
        bool IsCinlValid(string cin);
        bool IsAadhaarlValid(string aadhaar);

    }
}
