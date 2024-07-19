using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HiTechDistributionApp.BLL.entity;

namespace HiTechDistributionApp.VALIDATION
{
    public class BooksValidation
    {
        public static bool IsValidId(string id)
        {
            return (Regex.IsMatch(id, @"^\d+$"));
        }
        public static bool IsValidDescription(string input)
        {
            if (input.Length == 0 || input.Length > 50)
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

        public static bool IsValidTitle(string input)
        {
            if (input.Length == 0 || input.Length > 100)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidYear(string input)
        {
            int year = Convert.ToInt32(input);
            int currentYear = DateTime.Now.Year;

            if (year < 1950 || year > currentYear)
            {
                return false;
            }

            return true;
        }



        public static bool IsValidQuantity(string input, int bookId)
        {
            int quantity = Convert.ToInt32(input);

            if (quantity < 0)
            {
                return false;
            }else
            {   
                OrdersDetailController orderController = new OrdersDetailController();
                int quantityOpens = orderController.SerchOrderDetailOpenByBookId(bookId);

                if (quantityOpens > quantity)
                {
                    return false;
                }

            }          

            return true;
        }


        public static bool ExistsOrdersOpen(int bookId)
        {
                OrdersDetailController orderController = new OrdersDetailController();
                int quantityOpens = orderController.SerchOrderDetailOpenByBookId(bookId);

                if (quantityOpens > 0)
                {
                    return false;
                }

            return true;
        }


    }
}
