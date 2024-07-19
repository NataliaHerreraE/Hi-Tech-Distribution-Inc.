using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.BLL.entity;

namespace HiTechDistributionApp.DAL
{
    public class CustomerRepository
    {
        private readonly HiTechDistributionDBContext dBContext;

        public CustomerRepository()
        {
            dBContext = new HiTechDistributionDBContext();
        }

        public IEnumerable<Customer> GetCustomers() => dBContext.Customers.ToList();

            public Customer SearchCustomerById(int customerId) => dBContext.Customers.Find(customerId);

            public Customer SearchCustomerByName(string customerName) => dBContext.Customers.Where(c => c.CustomerName == customerName).FirstOrDefault();
       

    }
}
