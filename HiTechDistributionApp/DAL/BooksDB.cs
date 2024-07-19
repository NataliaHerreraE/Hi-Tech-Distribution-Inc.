using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.BLL;
using System.Data.SqlClient;

namespace HiTechDistributionApp.DAL
{
    public class BooksDB
    {
        public static int SaveRecord(Book book)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = connDB;
            cmdInsert.CommandText = "INSERT INTO Books (Title, Isbn, UnitPrice, PublicationYear, PublisherId, QuantityAvailable, CategoryId) " +
                                    "VALUES (@Title, @ISBN, @UnitPrice, @PublicationYear, @PublisherId, @QuantityAvailable, @CategoryId); " +
                                    "SELECT SCOPE_IDENTITY();";

            cmdInsert.Parameters.AddWithValue("@Title", book.Title);
            cmdInsert.Parameters.AddWithValue("@ISBN", book.Isbn);
            cmdInsert.Parameters.AddWithValue("@UnitPrice", book.UnitPrice);
            cmdInsert.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
            cmdInsert.Parameters.AddWithValue("@PublisherId", book.PublisherId);
            cmdInsert.Parameters.AddWithValue("@QuantityAvailable", book.QuantityAvailable);
            cmdInsert.Parameters.AddWithValue("@CategoryId", book.CategoryId);

            // ExecuteScalar() is used to retrieve a single value (for example, an aggregate value) from a database
            int bookId = Convert.ToInt32(cmdInsert.ExecuteScalar());

            connDB.Close();

            return bookId;
        }

        public static void UpdateRecord(Book book)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdUpdate = new SqlCommand();
            cmdUpdate.Connection = connDB;
            cmdUpdate.CommandText = "UPDATE Books " +
                                    "SET Title = @Title, " +
                                    "Isbn = @ISBN, " +
                                    "UnitPrice = @UnitPrice, " +
                                    "PublicationYear = @PublicationYear, " +
                                    "PublisherId = @PublisherId, " +
                                    "QuantityAvailable = @QuantityAvailable, " +
                                    "CategoryId = @CategoryId " +
                                    "WHERE BookId = @BookId";

            cmdUpdate.Parameters.AddWithValue("@BookId", book.BookId);
            cmdUpdate.Parameters.AddWithValue("@Title", book.Title);
            cmdUpdate.Parameters.AddWithValue("@ISBN", book.Isbn);
            cmdUpdate.Parameters.AddWithValue("@UnitPrice", book.UnitPrice);
            cmdUpdate.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
            cmdUpdate.Parameters.AddWithValue("@PublisherId", book.PublisherId);
            cmdUpdate.Parameters.AddWithValue("@QuantityAvailable", book.QuantityAvailable);
            cmdUpdate.Parameters.AddWithValue("@CategoryId", book.CategoryId);
            
            cmdUpdate.ExecuteNonQuery();
            connDB.Close();
        }

        public static void DeleteRecord(Book book)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            try
            {
                SqlCommand cmdDelete = new SqlCommand();
                cmdDelete.Connection = connDB;
                cmdDelete.CommandText = "DELETE FROM Books WHERE BookId = @BookId";
                cmdDelete.Parameters.AddWithValue("@BookId", book.BookId);
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

        public static List<Book> GetAllRecords() //revisar
        {
            List<Book> listBooks = new List<Book>();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelectAll = new SqlCommand(
                "SELECT b.BookID, b.Title, b.ISBN, b.UnitPrice, b.PublicationYear, b.QuantityAvailable, " +
                "p.PublisherName, c.Description " +
                "FROM Books b " +
                "INNER JOIN Publishers p ON b.PublisherID = p.PublisherID " +
                "INNER JOIN BooksCategories c ON b.CategoryID = c.CategoryID", connDB);

            SqlDataReader sqlReader = cmdSelectAll.ExecuteReader();
            while (sqlReader.Read())
            {
                Book book = new Book
                {
                    BookId = Convert.ToInt32(sqlReader["BookID"]),
                    Title = sqlReader["Title"].ToString(),
                    Isbn = Convert.ToInt64(sqlReader["ISBN"]),
                    UnitPrice = Convert.ToDecimal(sqlReader["UnitPrice"]),
                    PublicationYear = Convert.ToInt32(sqlReader["PublicationYear"]),
                    QuantityAvailable = Convert.ToInt32(sqlReader["QuantityAvailable"]),
                    PublisherName = sqlReader["PublisherName"].ToString(), 
                    Description = sqlReader["Description"].ToString() 
                };
                listBooks.Add(book);
            }
            connDB.Close();
            return listBooks;
        }


        public static Book SearchRecord(int input1)
        {
            Book book = new Book();
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;

            cmdSelect.CommandText = "SELECT b.BookId, b.Title, b.ISBN, b.UnitPrice, b.PublicationYear, b.QuantityAvailable, " +
                "p.PublisherName, c.Description " +
                "FROM Books b " +
                "INNER JOIN Publishers p ON b.PublisherId = p.PublisherId " +
                "INNER JOIN BooksCategories c ON b.CategoryId = c.CategoryId " +
                "WHERE BookId LIKE @BookId";

            cmdSelect.Parameters.AddWithValue("@BookId", input1);
            SqlDataReader sqlReader = cmdSelect.ExecuteReader();
            while (sqlReader.Read())
            {
                book = new Book();
                book.BookId = Convert.ToInt32(sqlReader["BookId"]);
                book.Title = sqlReader["Title"].ToString();
                book.Isbn = Convert.ToInt64(sqlReader["Isbn"]);
                book.UnitPrice = Convert.ToDecimal(sqlReader["UnitPrice"]);
                book.PublicationYear = Convert.ToInt32(sqlReader["PublicationYear"]);
                book.PublisherName = sqlReader["PublisherName"].ToString();
                book.QuantityAvailable = Convert.ToInt32(sqlReader["QuantityAvailable"]);
                book.Description = sqlReader["Description"].ToString();
            }
            connDB.Close();
            return book;
        }

        public enum SearchCriteria
        {
            Title,
            Publisher,
            Category
        }

        public static List<Book> SearchRecord(string input, string criteria)
        {
            List<Book> listBooks = new List<Book>();
            using (SqlConnection connDB = UtilityDB.ConnectDB())
            {
                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = connDB;

                // Switch between different search queries based on the criteria
                switch (criteria)
                {
                    case "Title":
                        cmdSelect.CommandText = "SELECT b.BookId, b.Title, b.ISBN, b.UnitPrice, b.PublicationYear, b.QuantityAvailable, " +
                                                "p.PublisherName, c.Description " +
                                                "FROM Books b " +
                                                "INNER JOIN Publishers p ON b.PublisherId = p.PublisherId " +
                                                "INNER JOIN BooksCategories c ON b.CategoryId = c.CategoryId " +
                                                "WHERE Title LIKE @SearchValue";
                        cmdSelect.Parameters.AddWithValue("@SearchValue", "%" + input + "%");

                        break;
                    case "Publisher":
                        cmdSelect.CommandText = "SELECT b.BookId, b.Title, b.ISBN, b.UnitPrice, b.PublicationYear, b.QuantityAvailable, " +
                                                "p.PublisherName, c.Description " +
                                                "FROM Books b " +
                                                "INNER JOIN Publishers p ON b.PublisherId = p.PublisherId " +
                                                "INNER JOIN BooksCategories c ON b.CategoryId = c.CategoryId " +
                                                "WHERE PublisherName LIKE @SearchValue"; 
                        break;
                    case "Category":
                        cmdSelect.CommandText = "SELECT b.BookId, b.Title, b.ISBN, b.UnitPrice, b.PublicationYear, b.QuantityAvailable, " +
                                                "p.PublisherName, c.Description " +
                                                "FROM Books b " +
                                                "INNER JOIN Publishers p ON b.PublisherId = p.PublisherId " +
                                                "INNER JOIN BooksCategories c ON b.CategoryId = c.CategoryId " +
                                                "WHERE Description LIKE @SearchValue";
                        break;
                }

                cmdSelect.Parameters.Clear();
                cmdSelect.Parameters.AddWithValue("@SearchValue", "%" + input + "%");



                using (SqlDataReader sqlReader = cmdSelect.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        Book book = new Book
                        {
                            BookId = Convert.ToInt32(sqlReader["BookId"]),
                            Title = sqlReader["Title"].ToString(),
                            Isbn = Convert.ToInt64(sqlReader["Isbn"]),
                            UnitPrice = Convert.ToDecimal(sqlReader["UnitPrice"]),
                            PublicationYear = Convert.ToInt32(sqlReader["PublicationYear"]),
                            PublisherName = sqlReader["PublisherName"].ToString(),
                            QuantityAvailable = Convert.ToInt32(sqlReader["QuantityAvailable"]),
                            Description = sqlReader["Description"].ToString()
                        };
                        listBooks.Add(book);
                    }
                }
            }
            return listBooks;
        }

        public static bool SearchISBN(long isbn)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmdSelect = new SqlCommand();
            cmdSelect.Connection = connDB;

            cmdSelect.CommandText = "SELECT COUNT(*) FROM Books WHERE ISBN = @ISBN";
            cmdSelect.Parameters.AddWithValue("@ISBN", isbn);

            int count = (int)cmdSelect.ExecuteScalar();

            connDB.Close();

            return count > 0;
        }


    }
}
