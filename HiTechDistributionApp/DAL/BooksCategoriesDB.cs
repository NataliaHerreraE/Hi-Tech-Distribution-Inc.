using HiTechDistributionApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HiTechDistributionApp.DAL
{
    public class BooksCategoriesDB
    {
        public static List<BookCategory> GetAllRecords()
        {
            List<BookCategory> list = new List<BookCategory>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand("select CategoryId, Description from BooksCategories", connDB);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            BookCategory bookCategory;

            while (sqlReader.Read())
            {
                bookCategory = new BookCategory();
                bookCategory.CategoryId = Convert.ToInt32(sqlReader["CategoryId"]);
                bookCategory.Description = sqlReader["Description"].ToString();
                list.Add(bookCategory);
            }
            connDB.Close();
            return list;
        }

        public static void SaveRecord(BookCategory bookCategory)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = connDB;
            cmdInsert.CommandText = "INSERT INTO BooksCategories (Description) " +
                                    "VALUES (@Description)";
            cmdInsert.Parameters.AddWithValue("@Description", bookCategory.Description);
            cmdInsert.ExecuteNonQuery();
            connDB.Close();
        }
        public static void UpdateRecord(BookCategory bookCategory)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = connDB;
            cmdUpdate.CommandText = "UPDATE BooksCategories " +
                                    "SET Description = @Description " +
                                    "WHERE CategoryId = @CategoryId";

            cmdUpdate.Parameters.AddWithValue("@CategoryId", bookCategory.CategoryId);
            cmdUpdate.Parameters.AddWithValue("@Description", bookCategory.Description);
            cmdUpdate.ExecuteNonQuery();
            connDB.Close();
        }
        public static void DeleteRecord(BookCategory bookCategory)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            try
            {
                SqlCommand cmdDelete = new SqlCommand();
                cmdDelete.Connection = connDB;
                cmdDelete.CommandText = "DELETE FROM BooksCategories WHERE CategoryId = @CategoryId";
                cmdDelete.Parameters.AddWithValue("@CategoryId", bookCategory.CategoryId);
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
        public static BookCategory SearchRecord(int input)
        {
            BookCategory bookCategory = new BookCategory();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;
            cmdSelect.CommandText = "SELECT * FROM BooksCategories WHERE CategoryId = @CategoryId";
            cmdSelect.Parameters.AddWithValue("@CategoryId", input);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();

            if (sqlReader.Read())
            {
                bookCategory.CategoryId = Convert.ToInt32(sqlReader["CategoryId"]);
                bookCategory.Description = sqlReader["Description"].ToString();
            }
            else
            {
                bookCategory = null;
            }
            connDB.Close();
            return bookCategory;
        }
        public static String GetDescriptionCategory(int categoryId)
        {
            String description = String.Empty;
            BookCategory bookCategory = new BookCategory();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;
            cmdSelect.CommandText = "SELECT Description FROM BooksCategories WHERE CategoryId = @CategoryId";
            cmdSelect.Parameters.AddWithValue("@CategoryId", categoryId);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();

            if (sqlReader.Read())
            {
                description = sqlReader["Description"].ToString();
            }
            connDB.Close();
            return description;
        }
        
    }
}
