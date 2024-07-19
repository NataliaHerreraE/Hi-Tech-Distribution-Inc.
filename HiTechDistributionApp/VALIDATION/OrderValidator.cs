using HiTechDistributionApp.BLL.entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HiTechDistributionApp.VALIDATION
{
    public class OrderValidator
    {
        public static bool IsValidId(string id)
        {
            return (Regex.IsMatch(id, @"^\d+$"));
        }

        public static bool isValidDate(string date)
        {
            return (Regex.IsMatch(date, @"^\d{4}-\d{2}-\d{2}$"));
        }

        public static bool isValidOrderType(string orderType)
        {
            switch (orderType)
            {
                case "Email":
                case "Phone":
                case "Fax":
                    return true;
                default:
                    return false;
            }
        }

        public static bool isValidStatus(string status)
        {
            switch (status)
            {
                case "In Process":
                case "Completed":
                case "Pending":
                case "Cancel":
                    return true;
                default:
                    return false;
            }
        }

        public static bool isValidQuantity(string quantity)
        {
            return (Regex.IsMatch(quantity, @"^\d+$"));
        }

        public static bool isValidPrice(string price)
        {
            // Remove currency symbol and other non-numeric characters except the decimal point and numbers
            string cleanPrice = Regex.Replace(price, @"[^\d\.]", "");

            // Now check if the cleaned price string is a valid price
            return Regex.IsMatch(cleanPrice, @"^\d+(\.\d{1,2})?$");
        }

        public static bool isValidItemSequencial(string itemSequencial, string orderId, IEnumerable<OrdersDetail> existingOrderDetails)
        {
            // Check if itemSequencial is a valid ID
            if (!IsValidId(itemSequencial))
            {
                return false;
            }


            int sequencial = int.Parse(itemSequencial);
            int order = int.Parse(orderId);

            // The itemSequencial is considered valid if no other order detail with the same order ID
            // has the same itemSequencial. Therefore, the condition should be "All" not "Any"
            // to return true only if all entries are different.
            return existingOrderDetails.All(od => !(od.ItemSequencial == sequencial && od.OrderID == order));

        }

        public static bool IsCloseOrder(string inputId)
        {
            int orderId = int.Parse(inputId);
            Order order = new OrderController().GetOrderById(orderId); 
            if (order != null)
            {
                return order.StatusId == 4 || order.StatusId == 9;
            }
            return false;
        }

    }
}
