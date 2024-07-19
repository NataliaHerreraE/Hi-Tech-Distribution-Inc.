using HiTechDistributionApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HiTechDistributionApp.DAL
{
    public class BooksAuthorsDB
    {
        public static List<BooksAuthors> GetAllBooksAuthors()
        {
            List<BooksAuthors> listBooksAuthors = new List<BooksAuthors>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectAll = new SqlCommand(
                                         "SELECT ba.BookId, b.Title, ba.AuthorId, a.FirstName, a.LastName " +
                                         "FROM BooksAuthors ba " +
                                         "INNER JOIN Books b ON ba.BookId = b.BookId " +
                                         "INNER JOIN Authors a ON ba.AuthorId = a.AuthorId", connDB);
            SqlDataReader sqlReader = cmdSelectAll.ExecuteReader();
            BooksAuthors booksAuthors;
            while (sqlReader.Read())
            {
                booksAuthors = new BooksAuthors();
                booksAuthors.BookId = Convert.ToInt32(sqlReader["BookId"]);
                booksAuthors.Title = sqlReader["Title"].ToString();
                booksAuthors.AuthorId = Convert.ToInt32(sqlReader["AuthorId"]);
                // Combine first name and last name to form the full name
                booksAuthors.AuthorFullName = sqlReader["FirstName"].ToString() + " " + sqlReader["LastName"].ToString();
                listBooksAuthors.Add(booksAuthors);
            }
            connDB.Close();
            return listBooksAuthors;
        }

        public static bool SaveRecord(int bookId, int authorId)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = connDB;
            cmdInsert.CommandText = "INSERT INTO BooksAuthors (BookId, AuthorId) " +
                                    "VALUES (@BookId, @AuthorId)";
            cmdInsert.Parameters.AddWithValue("@BookId", bookId);
            cmdInsert.Parameters.AddWithValue("@AuthorId", authorId);
            cmdInsert.ExecuteNonQuery();
            connDB.Close();
            return true;
        }
        
        public static bool DeleteRecord(int bookId, int authorId)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdDelete = new SqlCommand();
            cmdDelete.Connection = connDB;
            cmdDelete.CommandText = "DELETE FROM BooksAuthors " +
                                    "WHERE BookId = @BookId AND AuthorId = @AuthorId";
            cmdDelete.Parameters.AddWithValue("@BookId", bookId);
            cmdDelete.Parameters.AddWithValue("@AuthorId", authorId);
            cmdDelete.ExecuteNonQuery();
            connDB.Close();
            return true;
        }

        public static List<BooksAuthors> SearchRecords(int id, string searchBy)
        {
            List<BooksAuthors> listBooksAuthors = new List<BooksAuthors>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;
            cmdSelect.CommandText = "";

            // Set the SQL command text based on the search type
            if (searchBy == "Author ID")
            {
                cmdSelect.CommandText = "SELECT ba.BookId, b.Title, ba.AuthorId, a.FirstName, a.LastName " +
                                        "FROM BooksAuthors ba " +
                                        "INNER JOIN Books b ON ba.BookId = b.BookId " +
                                        "INNER JOIN Authors a ON ba.AuthorId = a.AuthorId " +
                                        "WHERE ba.AuthorId = @Id";
            }
            else if (searchBy == "Book ID")
            {
                cmdSelect.CommandText = "SELECT ba.BookId, b.Title, ba.AuthorId, a.FirstName, a.LastName " +
                                        "FROM BooksAuthors ba " +
                                        "INNER JOIN Books b ON ba.BookId = b.BookId " +
                                        "INNER JOIN Authors a ON ba.AuthorId = a.AuthorId " +
                                        "WHERE ba.BookId = @Id";
            }
            cmdSelect.Parameters.AddWithValue("@Id", id);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();

            while (sqlReader.Read())
            {
                BooksAuthors booksAuthors = new BooksAuthors();
                booksAuthors.BookId = Convert.ToInt32(sqlReader["BookId"]);
                booksAuthors.Title = sqlReader["Title"].ToString();
                booksAuthors.AuthorId = Convert.ToInt32(sqlReader["AuthorId"]);
                booksAuthors.AuthorFullName = $"{sqlReader["FirstName"]} {sqlReader["LastName"]}";
                listBooksAuthors.Add(booksAuthors);
            }
            connDB.Close();
            return listBooksAuthors;
        }
        public static List<BooksAuthors>SearchRecordByTitle(string input)
        {
            List<BooksAuthors> listBooksAuthors = new List<BooksAuthors>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;
            cmdSelect.CommandText = "SELECT ba.BookId, b.Title, ba.AuthorId, a.FirstName, a.LastName " +
                                    "FROM BooksAuthors ba " +
                                    "INNER JOIN Books b ON ba.BookId = b.BookId " +
                                    "INNER JOIN Authors a ON ba.AuthorId = a.AuthorId " +
                                    "WHERE b.Title LIKE @Title";
            cmdSelect.Parameters.AddWithValue("@Title", "%" + input + "%");
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            BooksAuthors booksAuthors;
            while (sqlReader.Read())
            {
                booksAuthors = new BooksAuthors();
                booksAuthors.BookId = Convert.ToInt32(sqlReader["BookId"]);
                booksAuthors.Title = sqlReader["Title"].ToString();
                booksAuthors.AuthorId = Convert.ToInt32(sqlReader["AuthorId"]);
                booksAuthors.AuthorFullName = sqlReader["FirstName"].ToString() + " " + sqlReader["LastName"].ToString();
                listBooksAuthors.Add(booksAuthors);
            }
            connDB.Close();
            return listBooksAuthors;
        }

        public static List<BooksAuthors>SearchRecordByAuthor(string input1, string input2)
        {
            List<BooksAuthors> listBooksAuthors = new List<BooksAuthors>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;
            cmdSelect.CommandText = "SELECT ba.BookId, b.Title, ba.AuthorId, a.FirstName, a.LastName " +
                                    "FROM BooksAuthors ba " +
                                    "INNER JOIN Books b ON ba.BookId = b.BookId " +
                                    "INNER JOIN Authors a ON ba.AuthorId = a.AuthorId " +
                                    "WHERE a.FirstName LIKE @FirstName AND a.LastName LIKE @LastName";
            cmdSelect.Parameters.AddWithValue("@FirstName", "%" + input1 + "%");
            cmdSelect.Parameters.AddWithValue("@LastName", "%" + input2 + "%");
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            BooksAuthors booksAuthors;
            while (sqlReader.Read())
            {
                booksAuthors = new BooksAuthors();
                booksAuthors.BookId = Convert.ToInt32(sqlReader["BookId"]);
                booksAuthors.Title = sqlReader["Title"].ToString();
                booksAuthors.AuthorId = Convert.ToInt32(sqlReader["AuthorId"]);
                booksAuthors.AuthorFullName = sqlReader["FirstName"].ToString() + " " + sqlReader["LastName"].ToString();
                listBooksAuthors.Add(booksAuthors);
            }
            connDB.Close();
            return listBooksAuthors;
        }
    }
}
