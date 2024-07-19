using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HiTechDistributionApp.VALIDATION
{
    internal class UserValidator
    {

        public static bool IsValidId(string id)
        {
            //Check if the input is a number
            return (Regex.IsMatch(id, @"^\d+$"));
        }
        public static bool IsValidPassword(string password)
        {
            //50 is the maximum length of the password and minimun is 8 with at least one uppercase, one lowercase, one number and one special character
            if (password.Length == 0 || password.Length > 50)
            {
                return false;
            }
            bool hasUpperCase = false;
            bool hasLowerCase = false;
            bool hasDigit = false;
            bool hasSpecialChar = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (Char.IsUpper(password[i]))
                {
                    hasUpperCase = true;
                }
                if (Char.IsLower(password[i]))
                {
                    hasLowerCase = true;
                }
                if (Char.IsDigit(password[i]))
                {
                    hasDigit = true;
                }
                if (Char.IsSymbol(password[i]) || Char.IsPunctuation(password[i]))
                {
                    hasSpecialChar = true;
                }
            }
            if (hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar)
            {
                return true;
            }
            return false;
            
        }

        public static bool IsValidDate(string date)
        {
            //validate date by yyyy-mm-dd
            return (Regex.IsMatch(date, @"^\d{4}$-(0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[0-2])"));
        }
    }
}
