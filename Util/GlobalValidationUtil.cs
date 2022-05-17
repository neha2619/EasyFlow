using Contracts;
using System;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Util
{
    public class GlobalValidationUtil :IGlobalValidationUtil
    {
        Regex MOBILE_PATTERN = new Regex(@"^[6-9]{1}[0-9]{9}");
        
        Regex EMAIL_PATTERN = new Regex(@"^[a-zA-Z0-9]+[.+-_]{0,1}[0-9a-zA-Z]+[@][a-zA-Z]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2,3}){0,1}");
        Regex GSTIN_PATTERN = new Regex(@"^[0-9]{15}");
        Regex CIN_PATTERN = new Regex(@"^[0-9a-zA-z]{21}");
        Regex AADHAAR_PATTERN = new Regex(@"^[0-9]{12}");
        Regex STRING_PATTERN = new Regex(@"^[a-zA-Z]");

        Regex hasNumber = new Regex(@"[0-9]+");
        Regex hasUpperChar = new Regex(@"[A-Z]+");
        Regex hassymbol = new Regex(@"[!@#$%^&*)(+_-]+");
        Regex hasMinimum8Chars = new Regex(@".{8,}");

        public bool IsEmailValid(string email)
        {
            if (email == null) 
            {
                return false;
            }
            return EMAIL_PATTERN.IsMatch(email);

        }
        public bool IsMobileValid(string mob)
        {
            if (mob == null)
            {
                return false;
            }
            return MOBILE_PATTERN.IsMatch(mob);
        }
        public bool IsPasswdStrong(string passwd)
        {
            if (passwd == null)
            {
                return false;
            }
            if (hasNumber.IsMatch(passwd) && hasUpperChar.IsMatch(passwd) && hassymbol.IsMatch(passwd) && hasMinimum8Chars.IsMatch(passwd))
            {
                return true;
            }
            return false;

        }
        public bool IsGstinValid(string gstin)
        {
            if (gstin == null)
            {
                return false;
            }
            return GSTIN_PATTERN.IsMatch(gstin);
        }
        public bool IsCinValid(string cin)
        {
            if (cin == null)
            {
                return false;
            }
            return CIN_PATTERN.IsMatch(cin);
        }

        public bool IsNumberValid(string num)
        {
            if (num == null)
            {
                return false;
            }
            return hasNumber.IsMatch(num);
        }
        
        public bool IsStringValid(string str)
        {
            if (str == null)
            {
                return false;
            }
            
            return STRING_PATTERN.IsMatch(str);
        }



        public bool IsAadhaarValid(string aadhaar)
        {
            if (aadhaar == null)
            {
                return false;
            }
            return AADHAAR_PATTERN.IsMatch(aadhaar);
        }
    }
}
