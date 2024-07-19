using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.BLL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HiTechDistributionApp.DAL
{
    public static class UserAccountsDB
    {
        public static void SaveRecord(UserAccount user)
        {
            SqlConnection conn = UtilityDB.ConnectDB();
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = conn;
            cmdInsert.CommandText = "INSERT INTO UserAccounts (UserId, Password, DateCreated, DateModified, StatusId) " +
                                    "VALUES (@UserId, @Password, @DateCreated, @DateModified, @StatusId)";

            cmdInsert.Parameters.AddWithValue("@UserId", user.UserId);
            cmdInsert.Parameters.AddWithValue("@Password", user.Password);
            cmdInsert.Parameters.AddWithValue("@DateCreated", DateTime.Now.Date);
            cmdInsert.Parameters.AddWithValue("@DateModified", DateTime.Now.Date);
            cmdInsert.Parameters.AddWithValue("@StatusId", user.StatusId);

            cmdInsert.ExecuteNonQuery();
            conn.Close();
        }

        public static void UpdateRecord(UserAccount user)
        {
            SqlConnection conn = UtilityDB.ConnectDB();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = conn;
            cmdUpdate.CommandText = "UPDATE UserAccounts SET Password = @Password, DateModified = @DateModified, StatusId = @StatusId " +
                                    "WHERE UserId = @UserId";
            cmdUpdate.Parameters.AddWithValue("@UserId", user.UserId);
            cmdUpdate.Parameters.AddWithValue("@Password", user.Password);
            cmdUpdate.Parameters.AddWithValue("@DateCreated", user.DateCreated);
            cmdUpdate.Parameters.AddWithValue("@DateModified", user.DateModified);
            cmdUpdate.Parameters.AddWithValue("@StatusId", user.StatusId);
            cmdUpdate.ExecuteNonQuery();
            conn.Close();
        }

        public static void UpdatePasswordByUser(UserAccount user)
        {
            SqlConnection conn = UtilityDB.ConnectDB();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = conn;
            cmdUpdate.CommandText = "UPDATE UserAccounts SET Password = @Password, DateModified = @DateModified " +
                                    "WHERE UserId = @UserId";
            cmdUpdate.Parameters.AddWithValue("@UserId", user.UserId);
            cmdUpdate.Parameters.AddWithValue("@Password", user.Password);
            cmdUpdate.Parameters.AddWithValue("@DateCreated", user.DateCreated);
            cmdUpdate.Parameters.AddWithValue("@DateModified", user.DateModified);
            cmdUpdate.ExecuteNonQuery();
            conn.Close();
        }


        public static void DeleteRecord(UserAccount user)
        {
            SqlConnection conn = UtilityDB.ConnectDB();
            try
            {
                SqlCommand cmdDelete = new SqlCommand();
                cmdDelete.Connection = conn;
                cmdDelete.CommandText = "DELETE FROM UserAccounts WHERE UserId = @UserId";
                cmdDelete.Parameters.AddWithValue("@UserId", user.UserId);
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<UserAccount> GetAllRecords()
        {
            List<UserAccount> listUser = new List<UserAccount>();
            SqlConnection conn = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand("select u.UserId, u.Password, u.DateCreated, u.DateModified, s.State " +
                                                    "from UserAccounts u " +
                                                    "inner join Status s on (u.StatusId = s.StatusId); ", conn);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            UserAccount user;
            while (sqlReader.Read())
            {
                user = new UserAccount();
                user.UserId = Convert.ToInt32(sqlReader["UserId"]);
                user.Password = sqlReader["Password"].ToString();
                user.DateCreated = Convert.ToDateTime(sqlReader["DateCreated"]);
                user.DateModified = Convert.ToDateTime(sqlReader["DateModified"]);
                user.StatusDescription = sqlReader["State"].ToString();
                listUser.Add(user);
            }
            conn.Close();
            return listUser;
        }

        

        public static UserAccount SearchRecord(int userId)
        {
            UserAccount user = new UserAccount();
            SqlConnection conn = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand("select u.UserId, u.Password, u.DateCreated, u.DateModified, s.State " +
                                                 "From UserAccounts u inner join Status s on (u.StatusId = s.StatusId) " +
                                                 "WHERE UserId = @UserId", conn);
            cmdSelect.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            
            if(sqlReader.Read())
            {
                user.UserId = Convert.ToInt32(sqlReader["UserId"]);
                user.Password = sqlReader["Password"].ToString();
                user.DateCreated = Convert.ToDateTime(sqlReader["DateCreated"]);
                user.DateModified = Convert.ToDateTime(sqlReader["DateModified"]);
                user.StatusDescription = sqlReader["State"].ToString();               
            }
            conn.Close();
            return user;
        }

        public static bool LoginUser(int userId, string password)
        {
            SqlConnection conn = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand("SELECT 1 FROM UserAccounts " +
                                                   "WHERE UserId = @UserId " +
                                                   "AND Password = @Password " +
                                                   "AND StatusId = 1 ", conn);
            cmdSelect.Parameters.AddWithValue("@UserId", userId);
            cmdSelect.Parameters.AddWithValue("@Password", password);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            if (sqlReader.Read())
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }

        public static String GetNameUser(int userId)
        {
            String userName = String.Empty;
            SqlConnection conn = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand("SELECT FirstName + ' ' + LastName as UserName " +
                                                  "FROM UserAccounts U " +
                                                  "inner join Employees E on(E.EmployeeId = U.UserId) " +
                                                  "WHERE UserId = @UserId " +
                                                  "AND U.StatusId = 1 ", conn);
            cmdSelect.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            if (sqlReader.Read())
            {
                userName = sqlReader["UserName"].ToString();
            }
            return userName;
        }

       
        public static List<UserAccount> GetAllStatus()
        {
            List<UserAccount> listUser = new List<UserAccount>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectAll = new SqlCommand("select State from Status ", connDB);
            SqlDataReader sqlReader = cmdSelectAll.ExecuteReader();
            UserAccount user;

            while (sqlReader.Read())
            {
                user = new UserAccount();
                user.StatusDescription = sqlReader["State"].ToString();
                listUser.Add(user);
            }
            connDB.Close();
            return listUser;

        }

    }
}
