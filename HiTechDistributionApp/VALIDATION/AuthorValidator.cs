using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HiTechDistributionApp.VALIDATION
{
    public class AuthorValidator
    {
        public static bool IsValidId(string id)
        {
            //Check if the input is a number
            return (Regex.IsMatch(id, @"^\d+$"));
        }

        public static bool IsValidName(string name)
        {
            if (name.Length == 0 || name.Length > 50)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < name.Length; i++)
                {
                    if ((!Char.IsLetter(name[i])) && (!Char.IsWhiteSpace(name[i])))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool IsValidEmail(string email)
        {
            return (Regex.IsMatch(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"));
        }
    }
}
