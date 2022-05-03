using Contracts;
using System;
using System.Text.RegularExpressions;

namespace Util
{
    public class GlobalValidationUtil :IGlobalValidationUtil
    {
        public static string MOBILE_PATTERN = "^[6-9]{1}[0-9]{9}";
        public static string EMAIL_PATTERN = "^[a-zA-Z0-9]+[.+-_]{0,1}[0-9a-zA-Z]+[@][a-zA-Z]+[.][a-zA-Z]{2,3}([.][a-zA-Z]{2,3}){0,1}";
        public static string GSTIN_PATTERN = "^[0-9]{15}";
        public static string CIN_PATTERN = "^[0-9a-zA-z]{21}";
        public static string AADHAAR_PATTERN = "^[0-9]{12}";
        public bool IsEmailValid(string email)
        {
            if (email == null) 
            {
                return false;
            }
            Regex regex = new Regex(EMAIL_PATTERN);
            return regex.IsMatch(email);
        }
        public bool IsMobilelValid(string mob)
        {
            if (mob == null)
            {
                return false;
            }
            Regex regex = new Regex(MOBILE_PATTERN);
            return regex.IsMatch(mob);
        }
        public bool IsGstinlValid(string gstin)
        {
            if (gstin == null)
            {
                return false;
            }
            Regex regex = new Regex(GSTIN_PATTERN);
            return regex.IsMatch(gstin);
        }
        public bool IsCinlValid(string cin)
        {
            if (cin == null)
            {
                return false;
            }
            Regex regex = new Regex(CIN_PATTERN);
            return regex.IsMatch(cin);
        }
        public bool IsAadhaarlValid(string aadhaar)
        {
            if (aadhaar == null)
            {
                return false;
            }
            Regex regex = new Regex(AADHAAR_PATTERN);
            return regex.IsMatch(aadhaar);
        }
    }
}
