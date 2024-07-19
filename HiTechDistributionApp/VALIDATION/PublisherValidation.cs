using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HiTechDistributionApp.VALIDATION
{
    public class PublisherValidation
    {
        public static bool IsValidId(string id)
        {
            return (Regex.IsMatch(id, @"^\d+$"));
        }
        public static bool IsValidPublisherName(string input)
        {
            if (input.Length == 0 || input.Length > 100)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if ((!Char.IsLetter(input[i])) && (!Char.IsWhiteSpace(input[i])))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
