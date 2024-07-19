using HiTechDistributionApp.BLL.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.DAL
{
    public class EmployeeRepository
    {
        private readonly HiTechDistributionDBContext dBContext;

        public EmployeeRepository()
        {
            dBContext = new HiTechDistributionDBContext();
        }

        public IEnumerable<Employee> GetEmployees() => dBContext.Employees.ToList();


    }
}
