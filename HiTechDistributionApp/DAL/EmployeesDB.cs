using HiTechDistributionApp.BLL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.DAL
{
    public static class EmployeesDB
    {
        public static void SaveRecord(Employee emp)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = connDB;
            cmdInsert.CommandText = "INSERT INTO Employees (FirstName, LastName, Email, JobId, StatusId) " +
                                    "VALUES (@FirstName, @LastName, @Email, @JobId, @StatusId)";

            cmdInsert.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmdInsert.Parameters.AddWithValue("@LastName", emp.LastName);
            cmdInsert.Parameters.AddWithValue("@Email", emp.Email);
            cmdInsert.Parameters.AddWithValue("@JobId", emp.JobId); // This is okay because it's a foreign key
            cmdInsert.Parameters.AddWithValue("@StatusId", emp.StatusId); // This is okay because it's a foreign key

            cmdInsert.ExecuteNonQuery();
            connDB.Close();
        }

        public static void UpdateRecord(Employee emp)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = connDB;
            cmdUpdate.CommandText = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, JobId = @JobId, StatusId = @StatusId " +
                                    "WHERE EmployeeId = @EmployeeId";

            cmdUpdate.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
            cmdUpdate.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmdUpdate.Parameters.AddWithValue("@LastName", emp.LastName);
            cmdUpdate.Parameters.AddWithValue("@Email", emp.Email);
            cmdUpdate.Parameters.AddWithValue("@JobId", emp.JobId); //  is a foreign key
            cmdUpdate.Parameters.AddWithValue("@StatusId", emp.StatusId); // is a foreign key


            cmdUpdate.ExecuteNonQuery();
            connDB.Close();
        }

        public static void DeleteRecord(Employee emp)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            try
            {
                SqlCommand cmdDelete = new SqlCommand();
                cmdDelete.Connection = connDB;
                cmdDelete.CommandText = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";
                cmdDelete.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                cmdDelete.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connDB.Close();
            }
        }

        public static List<Employee> GetAllRecords()
        {
            List<Employee> listEmp = new List<Employee>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectAll = new SqlCommand("select " +
                "                                   e.EmployeeID, e.FirstName, e.LastName, e.Email, j.JobTitle, s.State " +
                "                                   from Employees e inner join Jobs j on (e.JobId = j.JobId) " +
                "                                   inner join Status s on (e.StatusId = s.StatusId)", connDB);
            SqlDataReader sqlReader = cmdSelectAll.ExecuteReader();
            Employee emp;

            while (sqlReader.Read())
            {
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(sqlReader["EmployeeId"]);
                emp.FirstName = sqlReader["FirstName"].ToString();
                emp.LastName = sqlReader["LastName"].ToString();
                emp.Email = sqlReader["Email"].ToString();
                emp.JobTitle = sqlReader["JobTitle"].ToString();
                emp.StatusDescription = sqlReader["State"].ToString();
                listEmp.Add(emp);
            }
            connDB.Close();
            return listEmp;
        }

        public static List<Employee> SearchRecord(string input1, string input2)
        {
            List<Employee> listEmp = new List<Employee>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;
            cmdSelect.CommandText = "SELECT * FROM Employees WHERE FirstName LIKE @FirstName AND LastName LIKE @LastName";
            cmdSelect.Parameters.AddWithValue("@FirstName", "%" + input1 + "%");
            cmdSelect.Parameters.AddWithValue("@LastName", "%" + input2 + "%");
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            Employee emp;
            while (sqlReader.Read())
            {
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(sqlReader["EmployeeId"]);
                emp.FirstName = sqlReader["FirstName"].ToString();
                emp.LastName = sqlReader["LastName"].ToString();
                emp.Email = sqlReader["Email"].ToString();
                emp.JobId = Convert.ToInt32(sqlReader["JobId"]);
                emp.StatusId = Convert.ToInt32(sqlReader["StatusId"]);
                listEmp.Add(emp);
            }
            connDB.Close();
            return listEmp;
        }

        public static Employee SearchRecord(int inputEmployeeId)
        {
            Employee employee = new Employee();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;
            cmdSelect.CommandText = "SELECT e.EmployeeID, e.FirstName, e.LastName, e.Email, " +
                                          "j.JobTitle, s.State " +
                                          "FROM Employees e " +
                                              "inner join Jobs j on (e.JobId = j.JobId) " +
                                              "inner join Status s on (e.StatusId = s.StatusId) " +
                                              "WHERE EmployeeId = @EmployeeId";
            cmdSelect.Parameters.AddWithValue("@EmployeeId", inputEmployeeId);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();

            if (sqlReader.Read())
            {

                employee.EmployeeId = Convert.ToInt32(sqlReader["EmployeeId"]);
                employee.FirstName = sqlReader["FirstName"].ToString();
                employee.LastName = sqlReader["LastName"].ToString();
                employee.Email = sqlReader["Email"].ToString();
                employee.JobTitle = sqlReader["JobTitle"].ToString();
                employee.StatusDescription = sqlReader["State"].ToString();

            }
            else
            {
                employee = null;
            }

            connDB.Close();
            return employee;
        }

        public static String GetJobTitleEmployee(int inputEmployeeId)
        {
            String jobTitle = String.Empty;
            Employee employee = new Employee();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;
            cmdSelect.CommandText = "SELECT j.JobTitle " +
                                    "FROM Employees e " +
                                    "inner join Jobs j on (e.JobId = j.JobId) " +
                                    "WHERE EmployeeId = @EmployeeId " +
                                    "And e.StatusId in (6,7) ";
            cmdSelect.Parameters.AddWithValue("@EmployeeId", inputEmployeeId);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();

            if (sqlReader.Read())
            {
                jobTitle = sqlReader["JobTitle"].ToString();
            }
            return jobTitle;
        }

        public static List<Employee> GetAllJobs()
        {
            List<Employee> listEmp = new List<Employee>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectAll = new SqlCommand("select JobTitle from Jobs ", connDB);
            SqlDataReader sqlReader = cmdSelectAll.ExecuteReader();
            BLL.Employee emp;

            while (sqlReader.Read())
            {
                emp = new Employee();
                emp.JobTitle = sqlReader["JobTitle"].ToString();
                listEmp.Add(emp);
            }
            connDB.Close();
            return listEmp;

        }

        public static List<Employee> GetAllStatus()
        {
            List<Employee> listEmp = new List<Employee>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectAll = new SqlCommand("select State from Status ", connDB);
            SqlDataReader sqlReader = cmdSelectAll.ExecuteReader();
            Employee emp;

            while (sqlReader.Read())
            {
                emp = new Employee();
                emp.StatusDescription = sqlReader["State"].ToString();
                listEmp.Add(emp);
            }
            connDB.Close();
            return listEmp;

        }

    }
}
