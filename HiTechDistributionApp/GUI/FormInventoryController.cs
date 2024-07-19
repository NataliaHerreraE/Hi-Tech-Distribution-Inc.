using HiTechDistributionApp.BLL;
using HiTechDistributionApp.VALIDATION;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static HiTechDistributionApp.DAL.BooksDB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HiTechDistributionApp.GUI
{
    public partial class FormInventoryController : Form
    {
        private Dictionary<string, int> publisherDictionary;
        private Dictionary<string, int> bookCategoryDictionary;
        private int selectedCategoryId;
        private int selectedPublisherId;
        public FormInventoryController()
        {
            InitializeComponent();
        }
        FormUserProfile formUserProfile = new FormUserProfile();


        private void tabBookCategories_Click(object sender, EventArgs e)
        {

        }

        //Books tab

        private void btnAddBook_Click(object sender, EventArgs e)
        {


            if (!IsValidAuthor())
            {
                MessageBox.Show("Please select at least one author", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookId.Clear();
                txtBookId.Focus();
                return;
            }

            string input = comboBoxPublisherId.Text;
            if (!BooksValidation.IsValidPublisherName(input))
            {
                MessageBox.Show("Invalid PublishName, must not be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookId.Clear();
                txtBookId.Focus();
                return;
            }

            input = txtPublicationYear.Text;
            if (!BooksValidation.IsValidYear(input))
            {
                MessageBox.Show("Invalid Year, must not be null and between 1950 and", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookId.Clear();
                txtBookId.Focus();
                return;
            }


            input = comboBoxCategoryId.Text;
            if (!BooksValidation.IsValidDescription(input))
            {
                MessageBox.Show("Invalid Category, must not be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookId.Clear();
                txtBookId.Focus();
                return;
            }


            if (SearchISBN(Convert.ToInt64(txtISBN.Text)))
            {
                MessageBox.Show("The ISBN already exists. Duplicate data, please make changes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookId.Clear();
                txtBookId.Focus();
                return;
            }



            try
            {
                Book book = new Book(txtTitle.Text, Convert.ToInt64(txtISBN.Text), Convert.ToDecimal(txtUnitPrice.Text), Convert.ToInt32(txtPublicationYear.Text), selectedPublisherId, Convert.ToInt32(txtQuantityAvailable.Text), selectedCategoryId);
                int bookIdNew = book.SaveBook(book);
                MessageBox.Show("Registration created successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SaveAuthors(bookIdNew);
                ResetLoadAndMasterData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void SaveAuthors(int bookId)
        {
            BooksAuthors booksAuthors = new BooksAuthors();
            foreach (AuthorItem author in checkedListBoxAuthors.CheckedItems.OfType<AuthorItem>())
            {
                    booksAuthors.SaveRecord(bookId, author.Id);
            }
        }

        private void UpdateAuthors(int bookId)
        {
            BooksAuthors booksAuthors = new BooksAuthors();
            List<BooksAuthors> authorsList = booksAuthors.SearchRecords(bookId, "Book ID");

            // Get a list of selected author IDs
            List<int> selectedAuthorIds = checkedListBoxAuthors.CheckedItems.OfType<AuthorItem>().Select(a => a.Id).ToList();

            foreach (var author in authorsList)
            {
                // If the author from the database is not selected in the checkedListBoxAuthors
                if (!selectedAuthorIds.Contains(author.AuthorId))
                {
                    // Delete the record
                    booksAuthors.DeleteRecord(bookId, author.AuthorId);
                }
            }

            foreach (AuthorItem author in checkedListBoxAuthors.CheckedItems.OfType<AuthorItem>())
            {
                // If the selected author is not in the database
                if (!authorsList.Any(auth => auth.AuthorId == author.Id))
                {
                    // Save the record
                    booksAuthors.SaveRecord(bookId, author.Id);
                }
            }
        }

        private void btnUpdateBook_Click(object sender, EventArgs e)
        {
            string input = txtBookId.Text.Trim();
            if (!BooksValidation.IsValidId(input))
            {
                MessageBox.Show("Invalid Book ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookId.Clear();
                txtBookId.Focus();
                return;
            }

            input = txtQuantityAvailable.Text;
            int bookId = Convert.ToInt32(txtBookId.Text);
            if (!BooksValidation.IsValidQuantity(input, bookId))
            {
                MessageBox.Show("The chosen quantity is invalid. There are open orders that will not be fulfilled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookId.Clear();
                txtBookId.Focus();
                return;
            }

            try
            {
                Book book = new Book(Convert.ToInt32(txtBookId.Text), txtTitle.Text, Convert.ToInt64(txtISBN.Text), Convert.ToDecimal(txtUnitPrice.Text), Convert.ToInt32(txtPublicationYear.Text), selectedPublisherId, Convert.ToInt32(txtQuantityAvailable.Text), selectedCategoryId);
                book.UpdateBook(book);
                MessageBox.Show("Update data has been updated", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateAuthors(bookId);
                ResetLoadAndMasterData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            string input = txtBookId.Text.Trim();
            if (!BooksValidation.IsValidId(input))
            {
                MessageBox.Show("Invalid Book ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookId.Clear();
                txtBookId.Focus();
            }

            int bookId = Convert.ToInt32(txtBookId.Text);
            if (!BooksValidation.ExistsOrdersOpen(bookId))
            {
                MessageBox.Show("You cannot delete the book because there are open orders that will not be fulfilled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookId.Clear();
                txtBookId.Focus();
                return;
            }

            try
            {
                Book book = new Book(Convert.ToInt32(txtBookId.Text));
                book.DeleteBook(book);
                MessageBox.Show("Delete data has been updated", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetLoadAndMasterData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void comBoxBookSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = comBoxBookSearchBy.SelectedItem.ToString();

            txtBookSearch1.Visible = true;
            labelBookSearch1.Text = " Search by " + selectedText;
            labelBookSearch1.Visible = true;
        }

        private void btnSearchBook_Click(object sender, EventArgs e)
        {
            if (comBoxBookSearchBy.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comBoxBookSearchBy.Focus();
                return;
            }

            string selectedTextSearch, input = "";
            Book book = new Book();
            List<Book> listBook = new List<Book>();

            switch (comBoxBookSearchBy.SelectedIndex)
            {
                case 0:
                    input = txtBookSearch1.Text.Trim();
                    if (!BooksValidation.IsValidId(input))
                    {
                        MessageBox.Show("Invalid Book ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookSearch1.Clear();
                        txtBookSearch1.Focus();
                        return;
                    }
                    int bookId = Convert.ToInt32(input);
                    book = book.SearchBook(bookId);
                    DisplayInfoBook(book);
                    break;
                case 1:
                    input = txtBookSearch1.Text.Trim();
                    if (!BooksValidation.IsValidTitle(input))
                    {
                        MessageBox.Show("Invalid Title, must not be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookSearch1.Clear();
                        txtBookSearch1.Focus();
                        return;
                    }

                    selectedTextSearch = comBoxBookSearchBy.SelectedItem.ToString();
                    listBook = book.SearchBook(input, selectedTextSearch);
                    LoadAuthorName();
                    DisplayInfoBook(listBook, listViewBooks);
                    break;
                case 2:
                    input = txtBookSearch1.Text.Trim();
                    if (!BooksValidation.IsValidPublisherName(input))
                    {
                        MessageBox.Show("Invalid PublisherName, must not be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookSearch1.Clear();
                        txtBookSearch1.Focus();
                        return;
                    }

                    selectedTextSearch = comBoxBookSearchBy.SelectedItem.ToString();
                    listBook = book.SearchBook(input, selectedTextSearch);
                    LoadAuthorName();
                    DisplayInfoBook(listBook, listViewBooks);
                    break;
                case 3:
                    input = txtBookSearch1.Text.Trim();
                    if (!BooksValidation.IsValidDescription(input))
                    {
                        MessageBox.Show("Invalid Book ID, must not be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookSearch1.Clear();
                        txtBookSearch1.Focus();
                        return;
                    }
                    LoadAuthorName();
                    selectedTextSearch = comBoxBookSearchBy.SelectedItem.ToString();
                    listBook = book.SearchBook(input, selectedTextSearch);
                    DisplayInfoBook(listBook, listViewBooks);
                    break;
                default:
                    break;
            }
        }

        private void btnListAllBooks_Click(object sender, EventArgs e)
        {
            listViewBooks.Items.Clear();
            Book books = new Book();
            List<Book> listBook = books.GetAllBooks();
            DisplayInfoBook(listBook, listViewBooks);
        }

        private void DisplayInfoBook(Book book)
        {
            txtBookId.Text = book.BookId.ToString();
            txtTitle.Text = book.Title;
            txtISBN.Text = book.Isbn.ToString();
            txtUnitPrice.Text = book.UnitPrice.ToString();
            txtPublicationYear.Text = book.PublicationYear.ToString();
            comboBoxPublisherId.Text = book.PublisherName.ToString();
            txtQuantityAvailable.Text = book.QuantityAvailable.ToString();
            comboBoxCategoryId.Text = book.Description.ToString();
            AuthorsSelected(book);

        }

        private void AuthorsSelected(Book book)
        {
            checkedListBoxAuthors.Items.Clear();

            LoadAuthorName();
            int bookId = book.BookId;
            BooksAuthors booksAuthors = new BooksAuthors();

            List<BooksAuthors> authorsList = booksAuthors.SearchRecords(bookId, "Book ID");

            for (int i = 0; i < checkedListBoxAuthors.Items.Count; i++)
            {
                AuthorItem author = (AuthorItem)checkedListBoxAuthors.Items[i];
                if (authorsList.Any(auth => auth.AuthorId == author.Id))
                {
                    checkedListBoxAuthors.SetItemChecked(i, true);
                }
            }

        }

        private void DisplayInfoBook(List<Book> listBook, ListView listViewBooks)
        {
            listViewBooks.Items.Clear();

            if (listBook.Count != 0)
            {
                foreach (Book book in listBook)
                {
                    ListViewItem item = new ListViewItem(book.BookId.ToString());
                    item.SubItems.Add(book.Title);
                    item.SubItems.Add(book.Isbn.ToString());
                    item.SubItems.Add(book.UnitPrice.ToString());
                    item.SubItems.Add(book.PublicationYear.ToString());
                    item.SubItems.Add(book.PublisherName);
                    item.SubItems.Add(book.QuantityAvailable.ToString());
                    item.SubItems.Add(book.Description);
                    listViewBooks.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("No Book found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void loadComboBoxCategory()
        {
            comboBoxCategoryId.Items.Clear();
            BookCategory category = new BookCategory();
            bookCategoryDictionary = category.GetAllCategoriesWithIds();

            foreach (var pair in bookCategoryDictionary)
            {
                comboBoxCategoryId.Items.Add(pair.Key);
            }

        }

        //bookCategory
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string description = txtDescription.Text.Trim();
            if(!BooksValidation.IsValidDescription(description))
            {
                MessageBox.Show("Invalid Description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescription.Clear();
                txtDescription.Focus();
                return;
            }
            try
            {
                BookCategory category = new BookCategory(txtDescription.Text);
                category.SaveCategory(category);
                MessageBox.Show("Category has been saved successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadComboBoxCategory();
                ResetLoadAndMasterData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            string input = txtCategoryId.Text.Trim();
            if (!BooksValidation.IsValidId(input))
            {
                MessageBox.Show("Invalid Description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescription.Clear();
                txtDescription.Focus();
                return;
            }
            try
            {
                BookCategory category = new BookCategory(Convert.ToInt32(input), txtDescription.Text);
                category.UpdateCategory(category);
                MessageBox.Show("Category has been updated successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadComboBoxCategory();
                ResetLoadAndMasterData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string input = txtCategoryId.Text.Trim();
            if (!BooksValidation.IsValidId(input))
            {
                MessageBox.Show("Invalid Category ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescription.Clear();
                txtDescription.Focus();
                return;
            }
            try
            {
                BookCategory category = new BookCategory(Convert.ToInt32(input));
                category.DeleteCategory(category);
                MessageBox.Show("Category has been deleted successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetLoadAndMasterData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            string input = txtSearchCategory.Text.Trim();
            if (!BooksValidation.IsValidId(input))
            {
                MessageBox.Show("Invalid Category ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescription.Clear();
                txtDescription.Focus();
                return;
            }
            try
            {
                BookCategory category = new BookCategory();
                category = category.SearchCategory(Convert.ToInt32(input));
                if (category != null)
                {
                    txtCategoryId.Text = category.CategoryId.ToString();
                    txtDescription.Text = category.Description;
                }
                else
                {
                    MessageBox.Show("Category not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescription.Clear();
                    txtDescription.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListAllCategories_Click(object sender, EventArgs e)
        {
            listViewCategory.Items.Clear();
            try
            {
                BookCategory category = new BookCategory();
                List<BookCategory> listCategory = new List<BookCategory>();
                listCategory = category.GetAllCategories();
                if (listCategory.Count > 0)
                {
                    foreach (BookCategory cat in listCategory)
                    {
                        ListViewItem item = new ListViewItem(cat.CategoryId.ToString());
                        item.SubItems.Add(cat.Description);
                        listViewCategory.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("No categories found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //publishers

        private void btnAddPublisher_Click(object sender, EventArgs e)
        {
            if(!BooksValidation.IsValidPublisherName(txtPublisherName.Text))
            {
                MessageBox.Show("Invalid Publisher Name, Please start with Uppercase", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPublisherName.Clear();
                txtPublisherName.Focus();
                return;
            }
            try
            {
                Publisher publisher = new Publisher(txtPublisherName.Text);
                publisher.SavePublisher(publisher);
                MessageBox.Show("Publisher has been saved successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdatePublisher_Click(object sender, EventArgs e)
        {
            if (!BooksValidation.IsValidId(txtPublisherId.Text))
            {
                MessageBox.Show("Invalid Publisher ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPublisherName.Clear();
                txtPublisherName.Focus();
                return;
            }
            try
            {
                Publisher publisher = new Publisher(Convert.ToInt32(txtPublisherId.Text), txtPublisherName.Text);
                publisher.UpdatePublisher(publisher);
                MessageBox.Show("Publisher has been updated successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeletePublisher_Click(object sender, EventArgs e)
        {
            string input = txtPublisherId.Text.Trim();
            if (!BooksValidation.IsValidId(input))
            {
                MessageBox.Show("Invalid Publisher ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPublisherName.Clear();
                txtPublisherName.Focus();
                return;
            }
            try
            {
                Publisher publisher = new Publisher();
                publisher.PublisherId = Convert.ToInt32(input);
                publisher.DeletePublisher(publisher);
                MessageBox.Show("Publisher has been deleted successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetLoadAndMasterData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchPublisher_Click(object sender, EventArgs e)
        {
            string input = txtSearchPublisher.Text;
            if (!PublisherValidation.IsValidId(input))
            {
                MessageBox.Show("Invalid Publisher ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPublisherName.Clear();
                txtPublisherName.Focus();
                return;
            }
            try
            {
                Publisher publisher = new Publisher();;
                publisher = publisher.SearchPublisher(Convert.ToInt32(input));
                if (publisher != null)
                {
                    txtPublisherId.Text = publisher.PublisherId.ToString();
                    txtPublisherName.Text = publisher.PublisherName;
                }
                else
                {
                    MessageBox.Show("Publisher not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPublisherName.Clear();
                    txtPublisherName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListAllPublishers_Click(object sender, EventArgs e)
        {
            listViewPublisher.Items.Clear();
            try
            {
                Publisher publisher = new Publisher();
                List<Publisher> listPublisher = new List<Publisher>();
                listPublisher = publisher.GetAllPublishers();
                if (listPublisher.Count > 0)
                {
                    foreach (Publisher pub in listPublisher)
                    {
                        ListViewItem item = new ListViewItem(pub.PublisherId.ToString());
                        item.SubItems.Add(pub.PublisherName);
                        listViewPublisher.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("No publishers found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //booksAuthors

        private void btnSearchBooksAuthors_Click(object sender, EventArgs e)
        {
            if (comboBoxBooksAuthors.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comBoxBookSearchBy.Focus();
                return;
            }

            string selectedTextSearch, input = "";
            BooksAuthors bookAuthors = new BooksAuthors();
            List<BooksAuthors> listBookAuthor = new List<BooksAuthors>();

            switch (comboBoxBooksAuthors.SelectedIndex)
            {
                case 0:
                case 2:
                    input = txtSearchBooksAuthors1.Text.Trim();
                    if (!BooksValidation.IsValidId(input))
                    {
                        MessageBox.Show("Invalid Book ID or AuthorId, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSearchBooksAuthors1.Clear();
                        txtSearchBooksAuthors1.Focus();
                        return;
                    }
                    selectedTextSearch = comboBoxBooksAuthors.Text.Trim();
                    listBookAuthor = bookAuthors.SearchRecords(Convert.ToInt32(input), selectedTextSearch);
                    DisplayInfoBooksAuthors(listBookAuthor,listViewBooksAuthor);
                    break;
                case 1:
                    input = txtSearchBooksAuthors1.Text.Trim();
                    listBookAuthor = bookAuthors.SearchRecords(input);
                    DisplayInfoBooksAuthors(listBookAuthor, listViewBooksAuthor);
                    break;
                case 3:
                    input = txtSearchBooksAuthors1.Text.Trim();
                    string input2 = txtSearchBooksAuthors2.Text.Trim();
                    listBookAuthor = bookAuthors.SearchRecords(input, input2);
                    DisplayInfoBooksAuthors(listBookAuthor, listViewBooksAuthor);
                    break;
                default:
                    break;
            }
        }

        private void DisplayInfoBooksAuthors(List<BooksAuthors> listBook, ListView listViewBooksAuthor)
        {
            listViewBooksAuthor.Items.Clear();

            if (listBook.Count != 0)
            {
                foreach (BooksAuthors book in listBook)
                {
                    ListViewItem item = new ListViewItem(book.BookId.ToString());
                    item.SubItems.Add(book.Title);
                    item.SubItems.Add(book.AuthorId.ToString());
                    item.SubItems.Add(book.AuthorFullName);
                    listViewBooksAuthor.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("No BookAuthors found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListAllBooksAuthors_Click(object sender, EventArgs e)
        {
            listViewBooksAuthor.Items.Clear();
            try
            {
                BooksAuthors booksAuthors = new BooksAuthors();
                List<BooksAuthors> listBooksAuthors = new List<BooksAuthors>();
                listBooksAuthors = booksAuthors.GetAllBooksAuthors();
                if (listBooksAuthors.Count > 0)
                {
                    foreach (BooksAuthors reg in listBooksAuthors)
                    {
                        ListViewItem item = new ListViewItem(reg.BookId.ToString());
                        item.SubItems.Add(reg.Title);
                        item.SubItems.Add(reg.AuthorId.ToString());
                        item.SubItems.Add(reg.AuthorFullName);
                        listViewBooksAuthor.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("No publishers found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxBooksAuthors_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxBooksAuthors.SelectedIndex)
            {
                case 0:
                    labelSearchBooksAuthors1.Text = "Enter Book ID";
                    txtSearchBooksAuthors1.Visible = true;
                    labelSearchBooksAuthors1.Visible = true;
                    txtSearchBooksAuthors2.Visible = false;
                    labelSearchBooksAuthors2.Visible = false;
                    txtSearchBooksAuthors1.Clear();
                    txtSearchBooksAuthors1.Focus();
                    break;
                case 1:
                    labelSearchBooksAuthors1.Text = "Enter Book Title";
                    txtSearchBooksAuthors1.Visible = true;
                    labelSearchBooksAuthors1.Visible = true;
                    txtSearchBooksAuthors2.Visible = false;
                    labelSearchBooksAuthors2.Visible = false;
                    txtSearchBooksAuthors1.Clear();
                    txtSearchBooksAuthors1.Focus();
                    break;
                case 2:
                    labelSearchBooksAuthors1.Text = "Enter Author ID";
                    txtSearchBooksAuthors1.Visible = true;
                    labelSearchBooksAuthors1.Visible = true;
                    txtSearchBooksAuthors2.Visible = false;
                    labelSearchBooksAuthors2.Visible = false;
                    txtSearchBooksAuthors1.Clear();
                    txtSearchBooksAuthors1.Focus();
                    break;
                case 3:
                    labelSearchBooksAuthors1.Text = "Enter \nAuthor First Name";
                    labelSearchBooksAuthors2.Text = "Enter \nAuthor Last Name";
                    txtSearchBooksAuthors1.Visible = true;
                    labelSearchBooksAuthors1.Visible = true;
                    txtSearchBooksAuthors2.Visible = true;
                    labelSearchBooksAuthors2.Visible = true;
                    txtSearchBooksAuthors1.Clear();
                    txtSearchBooksAuthors1.Focus();
                    break;
                default:
                    break;
            }
        }

        //general
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit this app? ", "Exit ", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                this.Close();
                FormLogin loginForm = new FormLogin();
                loginForm.Show();

            }
        }

        private void FormInventoryController_Load(object sender, EventArgs e)
        {
            txtBookSearch1.Visible = false;
            labelBookSearch1.Visible = false;
            txtAuthorSearch1.Visible = false;
            txtAuthorSearch2.Visible = false;
            labelAuthorSearch1.Visible = false;
            labelAuthorSearch2.Visible = false;
            txtSearchBooksAuthors1.Visible = false;
            txtSearchBooksAuthors2.Visible = false;
            labelSearchBooksAuthors1.Visible = false;
            labelSearchBooksAuthors2.Visible = false;
            // Load user information
            LoadUserName(UserName);
            formUserProfile.UserId = UserName;

            // Reset and load master data including authors
            ResetLoadAndMasterData(); 

        }
        private void LoadUserName(int userId)
        {
            UserAccount user = new UserAccount();
            labelUserName.Text = user.GetUserNameUser(userId);
        }

        public int UserName { get; set; }

        private void txtBookSearch1_TextChanged(object sender, EventArgs e)
        {

        }


        private void loadComboBoxPublisher()
        {
            comboBoxPublisherId.Items.Clear();
            Publisher publisher = new Publisher();
            publisherDictionary = publisher.GetAllPublishersWithIds();

            foreach (var pair in publisherDictionary)
            {
                comboBoxPublisherId.Items.Add(pair.Key);
            }
        }

        private int getPublishrIdSelect()
        {
            if (comboBoxPublisherId.SelectedItem != null)
            {
                string selectedDescription = comboBoxPublisherId.SelectedItem.ToString();
                selectedPublisherId = publisherDictionary[selectedDescription];
                return selectedPublisherId;
            }
            else
            {
                return -1;
            }

        }

        private void comboBoxPublisherId_SelectedIndexChanged(object sender, EventArgs e)
        {
            getPublishrIdSelect();
        }

        private int getCategoryIdSelect()
        {
            if (comboBoxCategoryId.SelectedItem != null)
            {
                string selectedDescription = comboBoxCategoryId.SelectedItem.ToString();
                selectedCategoryId = bookCategoryDictionary[selectedDescription];
                return selectedCategoryId;
            }
            else
            {
                return -1;
            }

        }

        private void comboBoxCategoryId_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCategoryIdSelect();
        }

        // Author
        private void btnSearchAuthor_Click(object sender, EventArgs e)
        {
            if (comboBoxAuthorSearch.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxAuthorSearch.Focus();
                return;
            }

            string selectedTextSearch, input = "";
            Author author = new Author();
            List<Author> listAuthor = new List<Author>();

            switch (comboBoxAuthorSearch.SelectedIndex)
            {
                case 0:
                    input = txtAuthorSearch1.Text.Trim();
                    if (!AuthorValidator.IsValidId(input))
                    {
                        MessageBox.Show("Invalid Author ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookSearch1.Clear();
                        txtBookSearch1.Focus();
                        txtAuthorSearch2.Visible = false;
                        labelAuthorSearch2.Visible = false;
                        return;
                    }
                    int authorId = Convert.ToInt32(input);
                    author = author.SearchAuthor(authorId);
                    DisplayInfoAuthor(author);
                    break;
                case 1:
                    input = txtAuthorSearch1.Text.Trim();
                    if (!AuthorValidator.IsValidName(input))
                    {
                        MessageBox.Show("Invalid First Name, must not be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookSearch1.Clear();
                        txtBookSearch1.Focus();
                        return;
                    }

                    string input2 = txtAuthorSearch2.Text.Trim();
                    if (!AuthorValidator.IsValidName(input))
                    {
                        MessageBox.Show("Invalid Last Name, must not be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBookSearch1.Clear();
                        txtBookSearch1.Focus();
                        return;
                    }
                    listAuthor = author.SearchAuthor(input,input2);
                    DisplayInfoAuthor(listAuthor, listViewAuthors);
                    break;
                default:
                    break;
            }


        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            string input = txtAuthorFirstName.Text.Trim();
            if(!AuthorValidator.IsValidName(input))
            {
                MessageBox.Show("Invalid First Name, must not be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookSearch1.Clear();
                txtBookSearch1.Focus();
                return;
            }

            input = txtAuthorLastName.Text.Trim();
            if (!AuthorValidator.IsValidName(input))
            {
                MessageBox.Show("Invalid Last Name, must not be null and only character", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookSearch1.Clear();
                txtBookSearch1.Focus();
                return;
            }

            input = txtAuthorEmail.Text.Trim();
            if (!AuthorValidator.IsValidEmail(input))
            {
                MessageBox.Show("Invalid E-mail, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookSearch1.Clear();
                txtBookSearch1.Focus();
                return;
            }

            try
            {
                Author author = new Author(txtAuthorFirstName.Text,txtAuthorLastName.Text, txtAuthorEmail.Text);
                author.SaveAuthor(author);
                MessageBox.Show("Registration created successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetLoadAndMasterData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnUpdateAuthor_Click(object sender, EventArgs e)
        {
            string id = txtAuthorId.Text.Trim();
            if(!AuthorValidator.IsValidId(id))
            {
                MessageBox.Show("Invalid AuthorID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookSearch1.Clear();
                txtBookSearch1.Focus();
                return;
            }

            string input = txtAuthorFirstName.Text.Trim();
            if (!AuthorValidator.IsValidName(input))
            {
                MessageBox.Show("Invalid First Name, must not be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookSearch1.Clear();
                txtBookSearch1.Focus();
                return;
            }

            input = txtAuthorLastName.Text.Trim();
            if (!AuthorValidator.IsValidName(input))
            {
                MessageBox.Show("Invalid Last Name, must not be null and only character", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookSearch1.Clear();
                txtBookSearch1.Focus();
                return;
            }

            input = txtAuthorEmail.Text.Trim();
            if (!AuthorValidator.IsValidEmail(input))
            {
                MessageBox.Show("Invalid E-mail, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookSearch1.Clear();
                txtBookSearch1.Focus();
                return;
            }

            try
            {
                Author author = new Author(Convert.ToInt32(txtAuthorId.Text), txtAuthorFirstName.Text, txtAuthorLastName.Text, txtAuthorEmail.Text);
                author.UpdateAuthor(author);
                MessageBox.Show("Registration created successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetLoadAndMasterData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnDeleteAuthor_Click(object sender, EventArgs e)
        {
            string id = txtAuthorId.Text.Trim();
            if (!AuthorValidator.IsValidId(id))
            {
                MessageBox.Show("Invalid AuthorID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBookSearch1.Clear();
                txtBookSearch1.Focus();
                return;
            }
            try
            {
                Author author = new Author(Convert.ToInt32(txtAuthorId.Text));
                author.DeleteAuthor(author);
                MessageBox.Show("Registration created successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetLoadAndMasterData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void comboBoxAuthorSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText1 = comboBoxAuthorSearch.SelectedItem.ToString();
            if (comboBoxAuthorSearch.SelectedIndex == 0)
            {
                txtAuthorSearch1.Visible = true;
                labelAuthorSearch1.Visible = true;
                labelAuthorSearch1.Text = selectedText1;
                txtAuthorSearch2.Visible = false;
                labelAuthorSearch2.Visible = false;

            }
            else if (comboBoxAuthorSearch.SelectedIndex == 1)
            {
                txtAuthorSearch1.Visible = true;
                txtAuthorSearch2.Visible = true;
                labelAuthorSearch1.Visible= true;
                labelAuthorSearch2.Visible= true;
                labelAuthorSearch1.Text = "First Name";
                labelAuthorSearch2.Text = "Last Name";
            }


        }

        private void btnListAllAuthors_Click(object sender, EventArgs e)
        {
            listViewAuthors.Items.Clear();
            Author author = new Author();
            List<Author> listAuthor = author.GetAllAuthors();
            DisplayInfoAuthor(listAuthor, listViewAuthors);
        }

        private void DisplayInfoAuthor(Author author)
        {
            txtAuthorId.Text = author.AuthorId.ToString();
            txtAuthorFirstName.Text = author.FirstName.ToString();
            txtAuthorLastName.Text = author.LastName.ToString();
            txtAuthorEmail.Text = author.Email.ToString();
        }

        private void DisplayInfoAuthor(List<Author> listAuthor, ListView listViewAuthors)
        {
            listViewAuthors.Items.Clear();

            if (listAuthor.Count != 0)
            {
                foreach (Author author in listAuthor)
                {
                    ListViewItem item = new ListViewItem(author.AuthorId.ToString());
                    item.SubItems.Add(author.FirstName);
                    item.SubItems.Add(author.LastName);
                    item.SubItems.Add(author.Email);
                    listViewAuthors.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("No Author found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            FormUserProfile formUserProfile = new FormUserProfile();
            formUserProfile.UserId = UserName;
            formUserProfile.Show();
        }

        private void pictureBoxViewProfile_Click(object sender, EventArgs e)
        {
            FormUserProfile formUserProfile = new FormUserProfile();
            formUserProfile.UserId = UserName;
            formUserProfile.Show();
        }

        public bool IsValidAuthor()
        {
            if (checkedListBoxAuthors.CheckedItems.Count == 0)
            {
                return false;
            }
            return true;
        }

        private void LoadAuthorName()
        {
            checkedListBoxAuthors.Items.Clear();

            Author author = new Author();
            List<Author> listAuthor = author.GetAllAuthors();
            foreach (Author auth in listAuthor)
            {
                var authorItem = new AuthorItem { Id = auth.AuthorId, Name = auth.FirstName + " " + auth.LastName };
                if (!checkedListBoxAuthors.Items.OfType<AuthorItem>().Any(a => a.Id == authorItem.Id))
                {
                    checkedListBoxAuthors.Items.Add(authorItem);
                }
            }
        }

        private void ResetLoadAndMasterData()
        {

            txtAuthorEmail.Text = "";
            txtAuthorFirstName.Text = "";
            txtAuthorId.Text = "";
            txtAuthorLastName.Text = "";
            txtBookId.Text = "";
            txtISBN.Text = "";
            txtPublicationYear.Text = "";
            txtPublisherId.Text = "";
            txtQuantityAvailable.Text = "";
            txtTitle.Text = "";
            txtUnitPrice.Text = "";
            txtCategoryId.Text = "";
            txtDescription.Text = "";
            txtPublisherName.Text = "";
            txtSearchCategory.Text = "";
            txtSearchPublisher.Text = "";
            txtSearchBooksAuthors1.Text = "";
            txtSearchBooksAuthors2.Text = "";
            txtBookSearch1.Text = "";
            txtAuthorSearch1.Text = "";
            txtAuthorSearch2.Text = "";
            txtSearchBooksAuthors1.Text = "";
            txtSearchBooksAuthors2.Text = "";
            listViewAuthors.Items.Clear();
            listViewBooks.Items.Clear();
            listViewBooksAuthor.Items.Clear();
            listViewCategory.Items.Clear();
            listViewPublisher.Items.Clear();
            checkedListBoxAuthors.Items.Clear();
            loadComboBoxCategory();
            loadComboBoxPublisher();
            LoadAuthorName();

        }

        private void checkedListBoxAuthors_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
