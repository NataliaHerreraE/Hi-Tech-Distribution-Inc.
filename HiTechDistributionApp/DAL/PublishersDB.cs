using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.BLL;

namespace HiTechDistributionApp.DAL
{
    public class PublishersDB
    {
        public static void SaveRecord(Publisher publisher)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = connDB;
            cmdInsert.CommandText = "INSERT INTO Publishers (PublisherName) " +
                                    "VALUES (@PublisherName)";
            cmdInsert.Parameters.AddWithValue("@PublisherName", publisher.PublisherName);
            cmdInsert.ExecuteNonQuery();
            connDB.Close();
        }
        public static void UpdateRecord(Publisher publisher)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = connDB;
            cmdUpdate.CommandText = "UPDATE Publishers SET PublisherName = @PublisherName " +
                                    "WHERE PublisherId = @PublisherId";
            cmdUpdate.Parameters.AddWithValue("@PublisherId", publisher.PublisherId);
            cmdUpdate.Parameters.AddWithValue("@PublisherName", publisher.PublisherName);
            cmdUpdate.ExecuteNonQuery();
            connDB.Close();
        }
        public static void DeleteRecord(Publisher publisher)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            try
            {
                SqlCommand cmdDelete = new SqlCommand();
                cmdDelete.Connection = connDB;
                cmdDelete.CommandText = "DELETE FROM Publishers WHERE PublisherId = @PublisherId";
                cmdDelete.Parameters.AddWithValue("@PublisherId", publisher.PublisherId);
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
        public static List<Publisher> GetAllRecords()
        {
            List<Publisher> list = new List<Publisher>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand("select PublisherId, PublisherName from Publishers", connDB);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            Publisher publisher;
            while (sqlReader.Read())
            {
                publisher = new Publisher();
                publisher.PublisherId = Convert.ToInt32(sqlReader["PublisherId"]);
                publisher.PublisherName = sqlReader["PublisherName"].ToString();
                list.Add(publisher);
            }
            connDB.Close();
            return list;
        }
        public static Publisher SearchRecord(int input)
        {
            Publisher publisher = new Publisher();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;
            cmdSelect.CommandText = "SELECT PublisherId, PublisherName " +
                                    "FROM Publishers " +
                                    "WHERE PublisherId = @PublisherId";
            cmdSelect.Parameters.AddWithValue("@PublisherId", input);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            if (sqlReader.Read())
            {
                publisher.PublisherId = Convert.ToInt32(sqlReader["PublisherId"]);
                publisher.PublisherName = sqlReader["PublisherName"].ToString();
            }
            else
            {
                publisher = null;
            }
            connDB.Close();
            return publisher;
        }
    }
}
