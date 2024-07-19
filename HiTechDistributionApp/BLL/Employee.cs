using HiTechDistributionApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.BLL
{
    public class Employee
    {
        private int employeeId;
        private string firstName;
        private string lastName;
        private string email;
        private int jobId;
        private string jobTitle;
        private int statusId;
        private string statusDescription;

        public Employee()
        {
            employeeId = 0;
            firstName = string.Empty;
            lastName = string.Empty;
            email = string.Empty;
            jobId = 0;
            jobTitle = string.Empty;
            statusId = 0;
            statusDescription = string.Empty;
        }

        public Employee(string firstName, string lastName, string email, int jobId, int statusId)
        {

            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.jobId = jobId;
            this.statusId = statusId;
        }

        public Employee(int employeeId, string firstName, string lastName, string email, int jobId, int statusId)
        {
            this.employeeId = employeeId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.jobId = jobId;
            this.statusId = statusId;
        }

        public Employee(int employeeId)
        {
            this.employeeId = employeeId;
        }

        public int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int JobId
        {
            get { return jobId; }
            set { jobId = value; }
        }

        public string JobTitle
        {
            get { return jobTitle; }
            set { jobTitle = value; }
        }

        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }

        public string StatusDescription
        {
            get { return statusDescription; }
            set { statusDescription = value; }
        }

        public void SaveEmployee(Employee emp)
        {
            EmployeesDB.SaveRecord(emp);
        }

        public List<Employee> GetAllEmployee()
        {
            return EmployeesDB.GetAllRecords();
        }

        public void UpdateEmployee(Employee emp)
        {
            EmployeesDB.UpdateRecord(emp);
        }

        public void DeleteEmployee(Employee emp)
        {
            EmployeesDB.DeleteRecord(emp);
        }

        public Employee SearchEmployee(int employeeId)
        {
            return EmployeesDB.SearchRecord(employeeId);
        }

        public String GetJobTitle(int employeeId)
        {
            return EmployeesDB.GetJobTitleEmployee(employeeId);
        }
        public List<Employee> SearchEmployee(string input1, string input2)
        {
            return EmployeesDB.SearchRecord(input1, input2);
        }

        public List<Employee> GetAllJob()
        {
            return EmployeesDB.GetAllJobs();
        }

        public List<Employee> GetAllState()
        {
            return EmployeesDB.GetAllStatus();
        }
    }
}
