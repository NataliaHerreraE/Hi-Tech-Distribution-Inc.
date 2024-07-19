using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.DAL;

namespace HiTechDistributionApp.BLL
{
    public class Author
    {
        private int authorId;
        private string firstName;
        private string lastName;
        private string email;

        public Author()
        {
            authorId = 0;
            firstName = string.Empty;
            lastName = string.Empty;
            email = string.Empty;
        }

        public Author(int authorId, string firstName, string lastName, string email)
        {
            this.authorId = authorId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
        }
        
        public Author(int authorId)
        {
            this.authorId = authorId;
        }

        public Author(string firstName, string lastName, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
        }

        public int AuthorId
        {
            get { return authorId; }
            set { authorId = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public List<Author> GetAllAuthors()
        {
            return AuthorsDB.GetAllRecords();
        }

        public void SaveAuthor(Author author)
        {
            AuthorsDB.SaveRecord(author);
        }
        public void UpdateAuthor(Author author)
        {
            AuthorsDB.UpdateRecord(author);
        }
        public void DeleteAuthor(Author author)
        {
            AuthorsDB.DeleteRecord(author);
        }
        public Author SearchAuthor(int authorId)
        {
            return AuthorsDB.SearchRecord(authorId);
        }
        public List<Author> SearchAuthor(string input1, string input2)
        {
            return AuthorsDB.SearchRecord(input1, input2);
        }
    }
}
