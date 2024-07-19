using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.DAL;
using static HiTechDistributionApp.DAL.BooksDB;

namespace HiTechDistributionApp.BLL
{
    public class Book
    {
        private int bookId;
        private string title;
        private long isbn;
        private decimal unitPrice;
        private int publicationYear;
        private int publisherId;
        private int quantityAvailable;
        private int categoryId;
        private string publisherName;
        private string description;

        public Book()
        {
            bookId = 0;
            title = string.Empty;
            isbn = 0;
            unitPrice = 0;
            publicationYear = 0;
            publisherId = 0;
            quantityAvailable = 0;
            categoryId = 0;
            publisherName = string.Empty;
            description = string.Empty;
        }

        public Book(string title, long isbn, decimal unitPrice, int publicationYear, int publisherId, int quantityAvailable, int categoryId)
        {
            this.title = title;
            this.isbn = isbn;
            this.unitPrice = unitPrice;
            this.publicationYear = publicationYear;
            this.publisherId = publisherId;
            this.quantityAvailable = quantityAvailable;
            this.categoryId = categoryId;

        }
        public Book(int bookId, string title, long isbn, decimal unitPrice, int publicationYear, int publisherId, int quantityAvailable, int categoryId)
        {
            this.BookId = bookId;
            this.title = title;
            this.isbn = isbn;
            this.unitPrice = unitPrice;
            this.publicationYear = publicationYear;
            this.publisherId = publisherId;
            this.quantityAvailable = quantityAvailable;
            this.categoryId = categoryId;
        }

        public Book(int bookId)
        {
            this.BookId= bookId;
        }
        
        public int BookId
        {
            get { return bookId; }
            set { bookId = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public long Isbn
        {
            get { return isbn; }
            set { isbn = value; }
        }
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }
        public int PublicationYear
        {
            get { return publicationYear; }
            set { publicationYear = value; }
        }
        public int PublisherId
        {
            get { return publisherId; }
            set { publisherId = value; }
        }
        public int QuantityAvailable
        {
            get { return quantityAvailable; }
            set { quantityAvailable = value; }
        }
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }
        public string PublisherName
        {
            get { return publisherName; }
            set { publisherName = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public List<Book> GetAllBooks()
        {
            return BooksDB.GetAllRecords();
        }
        public int SaveBook(Book book)
        {
            return BooksDB.SaveRecord(book);
        }
        public void UpdateBook(Book book)
        {
            BooksDB.UpdateRecord(book);
        }
        public void DeleteBook(Book book)
        {
            BooksDB.DeleteRecord(book);
        }

        public Book SearchBook(int bookId)
        {
            return BooksDB.SearchRecord(bookId);
        }


        public List<Book> SearchBook(string input, string criteria)
        {
            return BooksDB.SearchRecord(input,criteria);
        }

        public bool SearchISBN(long isbn)
        {
            return BooksDB.SearchISBN(isbn);
        }

    }
}
