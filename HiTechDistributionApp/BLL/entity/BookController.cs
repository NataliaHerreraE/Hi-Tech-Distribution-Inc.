using HiTechDistributionApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.BLL.entity
{
    public class BookController
    {
        private readonly BookRepository bookRepository;

        public BookController()
        {
            bookRepository = new BookRepository();
        }

        public IEnumerable<Book> GetBooks() => bookRepository.GetBooks();

        public Dictionary<string, int> GetBookDictionary()
        {
            var book = bookRepository.GetBooks();
            if (book == null || !book.Any())
            {

                return new Dictionary<string, int>();
            }

            return book.ToDictionary(b => $"{b.Title} ({b.BookID})", b => b.BookID);
               
        }

        public string GetBookTitleById(int bookId)
        {
            var book = bookRepository.GetBooks().FirstOrDefault(b => b.BookID == bookId);
            return book != null ? $"{book.Title} ({book.BookID})" : null;
        }

        public decimal? GetUnitPriceByBookId(int bookId)
        {
            var book = GetBooks().FirstOrDefault(b => b.BookID == bookId);
            return book?.UnitPrice;
        }

        public void UpdateQuantity(int bookId, int newValue)
        {
            var book = bookRepository.GetBooks().FirstOrDefault(b => b.BookID == bookId);


            if (book != null)
            {
                book.QuantityAvailable += newValue;
                bookRepository.UpdateBook(book);
            }
        }

    }
}
