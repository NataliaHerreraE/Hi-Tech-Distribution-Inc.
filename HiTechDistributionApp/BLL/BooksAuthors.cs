using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.DAL;

namespace HiTechDistributionApp.BLL
{
    public class BooksAuthors
    {
        private int bookId;
        private int authorId;
        private string title;
        private string authorFullName;

        public BooksAuthors()
        {
            bookId = 0;
            authorId = 0;
        }
        public BooksAuthors(int bookId, int authorId)
        {
            this.bookId = bookId;
            this.authorId = authorId;
        }
        public BooksAuthors(int bookId, int authorId, string title, string authorFullName)
        {
            this.bookId = bookId;
            this.authorId = authorId;
            this.title = title;
            this.authorFullName = authorFullName;
        }

        public int BookId
        {
            get { return bookId; }
            set { bookId = value; }
        }

        public int AuthorId
        {
            get { return authorId; }
            set { authorId = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string AuthorFullName
        {
            get { return authorFullName; }
            set { authorFullName = value; }
        }

        public List<BooksAuthors> GetAllBooksAuthors()
        {
            return BooksAuthorsDB.GetAllBooksAuthors();
        }

        public bool SaveRecord(int bookId, int authorId)
        {
            return BooksAuthorsDB.SaveRecord(bookId, authorId);
        }

        public bool DeleteRecord(int bookId, int authorId)
        {
            return BooksAuthorsDB.DeleteRecord(bookId, authorId);
        }

        public List<BooksAuthors> SearchRecords(int id, string searchBy)
        {
            return BooksAuthorsDB.SearchRecords(id, searchBy);
        }
        
        public List<BooksAuthors> SearchRecords(string input)
        {
            return BooksAuthorsDB.SearchRecordByTitle(input);
        }

         public List<BooksAuthors> SearchRecords(string input, string input2)
        {
            return BooksAuthorsDB.SearchRecordByAuthor(input,input2);
        }


    }
}
