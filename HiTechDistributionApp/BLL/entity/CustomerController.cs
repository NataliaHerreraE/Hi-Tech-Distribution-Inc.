using HiTechDistributionApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.BLL.entity
{
    public class CustomerController
    {
        private readonly CustomerRepository customerRepository;

        public CustomerController()
        {
            customerRepository = new CustomerRepository();
        }

        public IEnumerable<Customer> GetCustomers() => customerRepository.GetCustomers();

        public Customer SearchCustomerById(int customerId) => customerRepository.SearchCustomerById(customerId);

        public Customer SearchCustomerByName(string customerName) => customerRepository.SearchCustomerByName(customerName);

        public Dictionary<string, int> GetCustomerDictionary()
        {
            var customers = customerRepository.GetCustomers(); 
            if (customers == null || !customers.Any())
            {
             
                return new Dictionary<string, int>();
            }
            return customers.ToDictionary(cust => $"{cust.CustomerName} ({cust.CustomerID})", cust => cust.CustomerID);
        }

    }
}
