using HiTechDistributionApp.BLL.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.DAL
{
    public class BookRepository
    {
        private readonly HiTechDistributionDBContext dBContext;

        public BookRepository()
        {
            dBContext = new HiTechDistributionDBContext();
        }

        public IEnumerable<Book> GetBooks() => dBContext.Books.ToList();

        public void UpdateBook(Book book)
        {
            if (book != null)
            {
                Book bookToUpdate = dBContext.Books.Find(book.BookID);
                if (bookToUpdate != null)
                {   bookToUpdate.QuantityAvailable = book.QuantityAvailable;
                    dBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Error: Book object is null.");
                }
            }
        }
    }
}
