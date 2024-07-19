using HiTechDistributionApp.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.DAL
{
    public class CustomerDB
    {
        public static List<Customer> GetAllCustomers()
        {
            List<Customer> listCustomer = new List<Customer>();
            SqlConnection conn = UtilityDB.ConnectDB();
            SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM Customers", conn);
            SqlDataReader reader = cmdSelectAll.ExecuteReader();
            while (reader.Read())
            {
                Customer customer = new Customer();
                customer.CustomerId = Convert.ToInt32(reader["CustomerID"]);
                customer.CustomerName = reader["CustomerName"].ToString();
                customer.Street = reader["Street"].ToString();
                customer.City = reader["City"].ToString();
                customer.PostalCode = reader["PostalCode"].ToString();
                customer.PhoneNumber = reader["PhoneNumber"].ToString();
                customer.FaxNumber = reader["FaxNumber"].ToString();
                customer.CreditLimit = Convert.ToDecimal(reader["CreditLimit"]);
                listCustomer.Add(customer);

            }
            conn.Close();
            return listCustomer;
        }
    }
}
