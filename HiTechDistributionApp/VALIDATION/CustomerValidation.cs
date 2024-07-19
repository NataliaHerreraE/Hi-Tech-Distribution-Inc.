using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HiTechDistributionApp.VALIDATION
{
    internal class CustomerValidation
    {

        public static bool IsValidId(string id)
        {
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
        public static bool IsValidNumberFormat(string phone)
        {
            return (Regex.IsMatch(phone, @"^\(\d{3}\)\d{3}-\d{4}$"));
        }
        public static bool IsValidPostalCode(string postalCode)
        {
            return (Regex.IsMatch(postalCode, @"^[ABCEGHJ-NPRSTVXY]\d[ABCEGHJ-NPRSTV-Z] \d[ABCEGHJ-NPRSTV-Z]\d$"));
        }
        public static bool IsValidCreditLimit(string creditLimit)
        {
            return Regex.IsMatch(creditLimit, @"^\d{1,10}(\.\d{1,2})?$");
        }
        public static bool IsValidCity(string city) 
        {
            if (city.Length == 0 || city.Length > 100)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < city.Length; i++)
                {
                    if ((!Char.IsLetter(city[i])) && (!Char.IsWhiteSpace(city[i])))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
       
        public static bool IsValidStreet(string street)
        {
            return (Regex.IsMatch(street, @"^[a-zA-Z0-9\s]{1,100}$"));
        }

    }
}
