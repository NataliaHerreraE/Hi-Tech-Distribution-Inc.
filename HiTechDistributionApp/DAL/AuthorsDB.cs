using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.BLL;

namespace HiTechDistributionApp.DAL
{
    public class AuthorsDB
    {
        public static void SaveRecord(Author author)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = connDB;
            cmdInsert.CommandText = "INSERT INTO Authors (FirstName, LastName, Email) " +
                                    "VALUES (@FirstName, @LastName, @Email)";

            cmdInsert.Parameters.AddWithValue("@FirstName", author.FirstName);
            cmdInsert.Parameters.AddWithValue("@LastName", author.LastName);
            cmdInsert.Parameters.AddWithValue("@Email", author.Email);
            cmdInsert.ExecuteNonQuery();
            connDB.Close();
        }

        public static void UpdateRecord(Author author)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = connDB;
            cmdUpdate.CommandText = "UPDATE Authors " +
                                    "SET FirstName = @FirstName, " +
                                    "LastName = @LastName, " +
                                    "Email = @Email " +
                                    "WHERE AuthorId = @AuthorId";
            cmdUpdate.Parameters.AddWithValue("@AuthorId", author.AuthorId);
            cmdUpdate.Parameters.AddWithValue("@FirstName", author.FirstName);
            cmdUpdate.Parameters.AddWithValue("@LastName", author.LastName);
            cmdUpdate.Parameters.AddWithValue("@Email", author.Email);
            cmdUpdate.ExecuteNonQuery();
            connDB.Close();
        }

        public static void DeleteRecord(Author author)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            try
            {
                SqlCommand cmdDelete = new SqlCommand();
                cmdDelete.Connection = connDB;
                cmdDelete.CommandText = "DELETE FROM Authors WHERE AuthorId = @AuthorId";
                cmdDelete.Parameters.AddWithValue("@AuthorId", author.AuthorId);
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

        public static List<Author> GetAllRecords()
        {
            List<Author> listAuthors = new List<Author>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand("select " +
                "AuthorId, FirstName, LastName, Email from Authors", connDB);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            Author author;

            while (sqlReader.Read())
            {
                author = new Author();
                author.AuthorId = Convert.ToInt32(sqlReader["AuthorId"]);
                author.FirstName = sqlReader["FirstName"].ToString();
                author.LastName = sqlReader["LastName"].ToString();
                author.Email = sqlReader["Email"].ToString();
                listAuthors.Add(author);
            }
            connDB.Close();
            return listAuthors;
        }

        public static Author SearchRecord(int input)
        {
            Author author = new Author();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;
            cmdSelect.CommandText = "SELECT * FROM Authors WHERE AuthorId = @AuthorId";
            cmdSelect.Parameters.AddWithValue("@AuthorId", input);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            while (sqlReader.Read())
            {
                author = new Author();
                author.AuthorId = Convert.ToInt32(sqlReader["AuthorId"]);
                author.FirstName = sqlReader["FirstName"].ToString();
                author.LastName = sqlReader["LastName"].ToString();
                author.Email = sqlReader["Email"].ToString();
            }
            connDB.Close();
            return author;
        }

        public static List<Author> SearchRecord(string input1, string input2)
        {
            List<Author> listAuthors = new List<Author>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;
            cmdSelect.CommandText = "SELECT * FROM Authors " +
                                    "WHERE FirstName LIKE @FirstName" +
                                    " AND LastName LIKE @LastName";
            cmdSelect.Parameters.AddWithValue("@FirstName", "%" + input1 + "%");
            cmdSelect.Parameters.AddWithValue("@LastName", "%" + input2 + "%");
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            Author author;
            while (sqlReader.Read())
            {
                author = new Author();
                author.AuthorId = Convert.ToInt32(sqlReader["AuthorId"]);
                author.FirstName = sqlReader["FirstName"].ToString();
                author.LastName = sqlReader["LastName"].ToString();
                author.Email = sqlReader["Email"].ToString();
                listAuthors.Add(author);
            }
            connDB.Close();
            return listAuthors;
        }
    }
}
