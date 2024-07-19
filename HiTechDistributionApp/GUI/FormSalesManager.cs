using HiTechDistributionApp.BLL;
using HiTechDistributionApp.DAL;
using HiTechDistributionApp.VALIDATION;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HiTechDistributionApp.GUI
{
    public partial class FormSalesManager : Form
    {
        SqlDataAdapter da;
        DataSet dsHiTechDistributionDB;
        DataTable dtCustomers;
        SqlCommandBuilder cmdBuilder;

        public FormSalesManager()
        {
            InitializeComponent();
        }
        FormUserProfile formUserProfile = new FormUserProfile();
        private void FormSalesManager_Load(object sender, EventArgs e)
        {   
            txtCustomerSearch.Visible = false;
            labelCustomerSearch.Visible = false;

            formUserProfile.UserId = UserName;
            dsHiTechDistributionDB = new DataSet("HiTechDistributionDB");
            dtCustomers = new DataTable("Customers");
            dsHiTechDistributionDB.Tables.Add(dtCustomers);
            dtCustomers.Columns.Add("CustomerID", typeof(int));
            dtCustomers.Columns.Add("CustomerName", typeof(string));
            dtCustomers.Columns.Add("Street", typeof(string));
            dtCustomers.Columns.Add("City", typeof(string));
            dtCustomers.Columns.Add("PostalCode", typeof(string));
            dtCustomers.Columns.Add("PhoneNumber", typeof(string));
            dtCustomers.Columns.Add("FaxNumber", typeof(string));
            dtCustomers.Columns.Add("CreditLimit", typeof(double));
            dtCustomers.PrimaryKey = new DataColumn[] { dtCustomers.Columns["CustomerID"] };
            dtCustomers.Columns["CustomerID"].AutoIncrement = true;
            dtCustomers.Columns["CustomerID"].AutoIncrementSeed = 1;
            dtCustomers.Columns["CustomerID"].AutoIncrementStep = 1;

            da = new SqlDataAdapter("SELECT * FROM Customers", UtilityDB.ConnectDB());
            cmdBuilder = new SqlCommandBuilder(da);
            da.InsertCommand = cmdBuilder.GetInsertCommand();
            da.UpdateCommand = cmdBuilder.GetUpdateCommand();
            da.DeleteCommand = cmdBuilder.GetDeleteCommand();
            da.Fill(dsHiTechDistributionDB, "Customers");
            LoadUserName(UserName);
        }
        private bool ValidateCostumerInput()
        {
            if(!CustomerValidation.IsValidName(txtCustomerName.Text))
            {
                MessageBox.Show("Invalid Customer Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerName.Clear();
                txtCustomerName.Focus();
                return false;
            }
            if (!CustomerValidation.IsValidStreet(txtStreet.Text))
            {
                MessageBox.Show("Invalid Street", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStreet.Clear();
                txtStreet.Focus();
                return false;
            }
            if (!CustomerValidation.IsValidCity(txtCity.Text))
            {
                MessageBox.Show("Invalid City", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCity.Clear();
                txtCity.Focus();
                return false;
            }
            if (!CustomerValidation.IsValidPostalCode(txtPostalCode.Text))
            {
                MessageBox.Show("Invalid Postal Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPostalCode.Clear();
                txtPostalCode.Focus();
                return false;
            }
            if (!CustomerValidation.IsValidNumberFormat(txtPhoneNumber.Text))
            {
                MessageBox.Show("Invalid Phone Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNumber.Clear();
                txtPhoneNumber.Focus();
                return false;
            }
            if (!CustomerValidation.IsValidNumberFormat(txtFaxNumber.Text))
            {
                MessageBox.Show("Invalid Fax Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFaxNumber.Clear();
                txtFaxNumber.Focus();
                return false;
            }
            if (!CustomerValidation.IsValidCreditLimit(txtCreditLimit.Text))
            {
                MessageBox.Show("Invalid Credit Limit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCreditLimit.Clear();
                txtCreditLimit.Focus();
                return false;
            }
            return true;
        }
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if(ValidateCostumerInput())
            {
                DataRow dr = dtCustomers.NewRow();
                dr["CustomerName"] = txtCustomerName.Text.Trim();
                dr["Street"] = txtStreet.Text.Trim();
                dr["City"] = txtCity.Text.Trim();
                dr["PostalCode"] = txtPostalCode.Text.Trim();
                dr["PhoneNumber"] = txtPhoneNumber.Text.Trim();
                dr["FaxNumber"] = txtFaxNumber.Text.Trim();
                dr["CreditLimit"] = Convert.ToDouble(txtCreditLimit.Text.Trim());
                dtCustomers.Rows.Add(dr);
                MessageBox.Show("Customer added successfully.", "Information");
                RefreshListView();
            }
            else
            {
                MessageBox.Show("Invalid data entered.", "Error");
            }
            UpdateDatabase();
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            if(CustomerValidation.IsValidId(txtCustomerId.Text)==false)
            {
                MessageBox.Show("Invalid Customer ID entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerId.Clear();
                txtCustomerId.Focus();
                return;
            }
            if(ValidateCostumerInput())
            {
                int customerId = Convert.ToInt32(txtCustomerId.Text.Trim());
                bool isCustomerFound = false;
                foreach (DataRow dr in dtCustomers.Rows)
                {
                    if (customerId == Convert.ToInt32(dr["CustomerID"]))
                    {
                        dr["CustomerName"] = txtCustomerName.Text.Trim();
                        dr["Street"] = txtStreet.Text.Trim();
                        dr["City"] = txtCity.Text.Trim();
                        dr["PostalCode"] = txtPostalCode.Text.Trim();
                        dr["PhoneNumber"] = txtPhoneNumber.Text.Trim();
                        dr["FaxNumber"] = txtFaxNumber.Text.Trim();
                        dr["CreditLimit"] = Convert.ToDouble(txtCreditLimit.Text.Trim());
                        isCustomerFound = true;
                        MessageBox.Show("Customer updated successfully.", "Information");
                        break;
                    }
                    
                }
                RefreshListView();
                if (!isCustomerFound)
                {
                    MessageBox.Show("Customer not found.", "Error");
                }
                UpdateDatabase();
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if(CustomerValidation.IsValidId(txtCustomerId.Text)==false)
            {
                MessageBox.Show("Invalid Customer ID entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerId.Clear();
                txtCustomerId.Focus();
                return;
            }
            else
            {
                int customerId = Convert.ToInt32(txtCustomerId.Text.Trim());
                bool isCustomerFound = false;
                foreach (DataRow dr in dtCustomers.Rows)
                {
                    if (customerId == Convert.ToInt32(dr["CustomerID"]))
                    {
                        dr.Delete();
                        isCustomerFound = true;
                        MessageBox.Show("Customer deleted successfully.", "Information");
                        break;
                    }
                }
                RefreshListView();
                if (!isCustomerFound)
                {
                    MessageBox.Show("Customer not found.", "Error");
                    return; 
                }

            }
            UpdateDatabase();
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            if (comBoxCustomerSearchBy.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comBoxCustomerSearchBy.Focus();
                return;
            }
            string input = "";
            Customer customer = new Customer();
            List<Customer> listCustomer = new List<Customer>();
            switch (comBoxCustomerSearchBy.SelectedIndex) 
            {                 
                case 0:
                input = txtCustomerSearch.Text.Trim();
                if (!CustomerValidation.IsValidId(input))
                    {
                        MessageBox.Show("Invalid Customer ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCustomerSearch.Clear();
                        txtCustomerSearch.Focus();
                        return;
                    }
                    int customerId = Convert.ToInt32(input);
                    bool customerIdFound = false;
                    da.SelectCommand = new SqlCommand("SELECT * FROM Customers WHERE CustomerID = @CustomerID", UtilityDB.ConnectDB());
                    da.SelectCommand.Parameters.AddWithValue("@CustomerId", customerId);
                    da.Fill(dsHiTechDistributionDB, "Customers");
                    
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        if (customerId == Convert.ToInt32(dr["CustomerID"]))
                        {
                            txtCustomerId.Text = dr["CustomerID"].ToString();
                            txtCustomerName.Text = dr["CustomerName"].ToString();
                            txtStreet.Text = dr["Street"].ToString();
                            txtCity.Text = dr["City"].ToString();
                            txtPostalCode.Text = dr["PostalCode"].ToString();
                            txtPhoneNumber.Text = dr["PhoneNumber"].ToString();
                            txtFaxNumber.Text = dr["FaxNumber"].ToString();
                            txtCreditLimit.Text = dr["CreditLimit"].ToString();
                            customerIdFound = true;
                            break;
                        }
                    }
                    if (!customerIdFound)
                    {
                        MessageBox.Show("Customer ID not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                    
                case 1:
                    input = txtCustomerSearch.Text.Trim();
                    if (!CustomerValidation.IsValidName(input))
                    {
                        MessageBox.Show("Invalid Customer Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCustomerSearch.Clear();
                        txtCustomerSearch.Focus();
                        return;
                    }
                    da.SelectCommand = new SqlCommand("SELECT * FROM Customers WHERE CustomerName = @CustomerName", UtilityDB.ConnectDB());
                    da.SelectCommand.Parameters.AddWithValue("@CustomerName", input);
                    da.Fill(dsHiTechDistributionDB, "Customers");
                    string customerName = input;
                    bool customerNameFound = false;
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        if (customerName == dr["CustomerName"].ToString())
                        {
                            //txtCustomerId.Text = dr["CustomerID"].ToString();
                            txtCustomerName.Text = dr["CustomerName"].ToString();
                            txtStreet.Text = dr["Street"].ToString();
                            txtCity.Text = dr["City"].ToString();
                            txtPostalCode.Text = dr["PostalCode"].ToString();
                            txtPhoneNumber.Text = dr["PhoneNumber"].ToString();
                            txtFaxNumber.Text = dr["FaxNumber"].ToString();
                            txtCreditLimit.Text = dr["CreditLimit"].ToString();
                            customerNameFound = true;
                            break;
                        }
                    }
                    if (!customerNameFound)
                    {
                        MessageBox.Show("Customer Name not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
                default:
                    break;
            }
        }

        private void btnUpdateDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                if (dsHiTechDistributionDB.HasChanges())
                {
                    da.Update(dsHiTechDistributionDB, "Customers");
                    MessageBox.Show("Database updated successfully", "Database updated");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the database: {ex.Message}", "Update Error");
            }
        }

        private void UpdateDatabase()
        {
            try
            {
                if (dsHiTechDistributionDB.HasChanges())
                {
                    da.Update(dsHiTechDistributionDB, "Customers");
                    MessageBox.Show("Database updated successfully", "Database updated");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the database: {ex.Message}", "Update Error");
            }
        }


        private void RefreshListView()
        {
            listViewCustomer.Items.Clear();  // Clear existing items

            // Iterate through the DataTable rows and add them to the ListView
            foreach (DataRow dr in dtCustomers.Rows)
            {
                // Skip any rows that have been marked for deletion
                if (dr.RowState != DataRowState.Deleted)
                {
                    ListViewItem item = new ListViewItem(dr["CustomerID"].ToString());
                    item.SubItems.Add(dr["CustomerName"].ToString());
                    item.SubItems.Add(dr["Street"].ToString());
                    item.SubItems.Add(dr["City"].ToString());
                    item.SubItems.Add(dr["PostalCode"].ToString());
                    item.SubItems.Add(dr["PhoneNumber"].ToString());
                    item.SubItems.Add(dr["FaxNumber"].ToString());
                    item.SubItems.Add(dr["CreditLimit"].ToString());
                    listViewCustomer.Items.Add(item);
                }
            }
        }

        private void btnListCustomerDS_Click(object sender, EventArgs e)
        {
            da.SelectCommand = new SqlCommand("SELECT * FROM Customers", UtilityDB.ConnectDB());
            da.Fill(dsHiTechDistributionDB, "Customers");
            listViewCustomer.Items.Clear();
            foreach (DataRow dr in dtCustomers.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    ListViewItem item = new ListViewItem(dr["CustomerID"].ToString());
                    item.SubItems.Add(dr["CustomerName"].ToString());
                    item.SubItems.Add(dr["Street"].ToString());
                    item.SubItems.Add(dr["City"].ToString());
                    item.SubItems.Add(dr["PostalCode"].ToString());
                    item.SubItems.Add(dr["PhoneNumber"].ToString());
                    item.SubItems.Add(dr["FaxNumber"].ToString());
                    item.SubItems.Add(dr["CreditLimit"].ToString());
                    listViewCustomer.Items.Add(item);
                }
            }
        }

        private void btnListCustomerDB_Click(object sender, EventArgs e)
        {
            listViewCustomer.Items.Clear();
            Customer customer = new Customer();
            List<Customer> listCustomer = customer.GetAllCustomers();
            DisplayInfoCustomer(listCustomer, listViewCustomer);
        }

        private void DisplayInfoCustomer(List<Customer> listCustomer, ListView listCostumerV)
        {
            listCostumerV.Items.Clear();

            if (listCustomer.Count != 0)
            {
                foreach (Customer customer in listCustomer)
                {
                    ListViewItem item = new ListViewItem(customer.CustomerId.ToString());
                    item.SubItems.Add(customer.CustomerName);
                    item.SubItems.Add(customer.Street);
                    item.SubItems.Add(customer.City);
                    item.SubItems.Add(customer.PostalCode);
                    item.SubItems.Add(customer.PhoneNumber);
                    item.SubItems.Add(customer.FaxNumber);
                    item.SubItems.Add(customer.CreditLimit.ToString());
                    listCostumerV.Items.Add(item);
                }
                
            }  
            else
            {
                MessageBox.Show("No record found in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void comBoxCustomerSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comBoxCustomerSearchBy.SelectedIndex)
            {
                case 0:
                    labelCustomerSearch.Text = "Enter Customer ID";
                    labelCustomerSearch.Visible = true;
                    txtCustomerSearch.Visible = true;
                    txtCustomerSearch.Clear();
                    txtCustomerSearch.Focus();
                    break;
                case 1:
                    labelCustomerSearch.Text = "Enter Customer Name";
                    labelCustomerSearch.Visible = true;
                    txtCustomerSearch.Visible = true;
                    txtCustomerSearch.Clear();
                    txtCustomerSearch.Focus();
                    break;
                default:
                    break;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit this app? ", "Exit ", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                this.Close();
                FormLogin loginForm = new FormLogin();
                loginForm.Show();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void LoadUserName(int userId)
        {
            UserAccount user = new UserAccount();
            labelUserName.Text = user.GetUserNameUser(userId);
        }

        public int UserName { get; set; }

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
    }
}
