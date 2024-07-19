using HiTechDistributionApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.BLL.entity
{
    public class EmployeeController
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeeController()
        {
            employeeRepository = new EmployeeRepository();
        }

        public IEnumerable<Employee> GetCustomers() => employeeRepository.GetEmployees();

        public Dictionary<string, int> GetEmployeeDictionary()
        {
            var employees = employeeRepository.GetEmployees(); 
            if (employees == null || !employees.Any())
            {
               
                return new Dictionary<string, int>();
            }
           
            return employees.ToDictionary(emp => $"{emp.FirstName} ({emp.EmployeeID})", emp => emp.EmployeeID);
        }

    }
}
