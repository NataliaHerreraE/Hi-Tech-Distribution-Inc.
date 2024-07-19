using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiTechDistributionApp.DAL;

namespace HiTechDistributionApp.BLL
{
    public class BookCategory
    {
        private int categoryId;
        private string description;

        public BookCategory()
        {
            categoryId = 0;
            description = string.Empty;
        }
        public BookCategory(string description)
        {
            this.description = description;
        }
        public BookCategory(int input)
        {
            this.CategoryId = input;
        }

        public BookCategory(int categoryId, string description)
        {
            this.categoryId = categoryId;
            this.description = description;
        }

        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public List<BookCategory> GetAllCategories()
        {
            return BooksCategoriesDB.GetAllRecords();
        }
        public void SaveCategory(BookCategory category)
        {
            BooksCategoriesDB.SaveRecord(category);
        }
        public void UpdateCategory(BookCategory category)
        {
            BooksCategoriesDB.UpdateRecord(category);
        }
        public void DeleteCategory(BookCategory category)
        {
            BooksCategoriesDB.DeleteRecord(category);
        }
        public BookCategory SearchCategory(int categoryId)
        {
            return BooksCategoriesDB.SearchRecord(categoryId);
        }
        public String GetCategoryDescription(int categoryId)
        {
            return BooksCategoriesDB.GetDescriptionCategory(categoryId);
        }
        public Dictionary<string, int> GetAllCategoriesWithIds()
        {
            List<BookCategory> categories = BooksCategoriesDB.GetAllRecords();
            Dictionary<string, int> categoriesDictionary = new Dictionary<string, int>();
            foreach (BookCategory category in categories)
            {
                categoriesDictionary.Add(category.Description, category.CategoryId);
            }
            return categoriesDictionary;
        }

    }
}
