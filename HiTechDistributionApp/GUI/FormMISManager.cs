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

namespace HiTechDistributionApp.GUI
{
    public partial class FormMISManager : Form
    {
        private Dictionary<string, int> stateDictionary;
        private Dictionary<string, int> jobsDictionary;
        private Dictionary<string, int> userStateDictionary;
        private int selectedJobId;
        private int selectedStatusId;

        public FormMISManager()
        {
            InitializeComponent();
            loadComboBoxState();
        }
        
        private void FormMISManager_Load(object sender, EventArgs e)
        {
            labelEmpSearch1.Visible = false;
            labelEmpSearch2.Visible = false;
            txtEmployeeSearch1.Visible = false;
            txtEmployeeSearch2.Visible = false;
            LoadUserName(UserName);
            formUserProfile.UserId = UserName;
            loadComboBoxJobs();
            loadComboBoxState();

        }

        FormUserProfile formUserProfile = new FormUserProfile();
        //Employee mini form
        private bool ValidateEmployeeInput()
        {

            string input = txtFirstName.Text.Trim();
            if (!EmployeeValidator.IsValidName(input))
            {
                MessageBox.Show("Invalid First Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Clear();
                txtFirstName.Focus();
                return false;
            }

            input = txtLastName.Text.Trim();
            if (!EmployeeValidator.IsValidName(input))
            {
                MessageBox.Show("Invalid Last Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Clear();
                txtLastName.Focus();
                return false;
            }

            input = txtEmail.Text.Trim();
            if (!EmployeeValidator.IsValidEmail(input))
            {
                MessageBox.Show("Invalid Email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Clear();
                txtEmail.Focus();
                return false;
            }

            input = getJobIdEmployeeSelect().ToString();
            if (!EmployeeValidator.IsValidId(input))
            {
                MessageBox.Show("Invalid Job ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxJobEmp.Focus();
                return false;
            }

            input = getStateIdEmployee().ToString();
            if (!EmployeeValidator.IsValidId(input))
            {
                MessageBox.Show("Invalid Status ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxStateIdEmp.Focus();
                return false;
            }


            return true;
        }

        private void btnSaveEmployee_Click(object sender, EventArgs e)
        {
            if (!ValidateEmployeeInput())
            {
                return;
            }

            try
            {
                selectedStatusId = getStateIdEmployee();
                selectedJobId = getJobIdEmployeeSelect();
                Employee employee = new Employee(txtFirstName.Text, txtLastName.Text, txtEmail.Text, selectedJobId, selectedStatusId);
                employee.SaveEmployee(employee);
                MessageBox.Show("Employee data has been saved", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            string input = txtEmployeeId.Text.Trim();
            if (!EmployeeValidator.IsValidId(input))
            {
                MessageBox.Show("Invalid Employee ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmployeeId.Clear();
                txtEmployeeId.Focus();
            }

            if (!ValidateEmployeeInput())
            {
                return;
            }

            try
            {
                Employee employee = new Employee(Convert.ToInt32(txtEmployeeId.Text), txtFirstName.Text, txtLastName.Text, txtEmail.Text, selectedJobId, selectedStatusId);
                employee.UpdateEmployee(employee);
                MessageBox.Show("Employee data has been updated", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            string input = "";
            input = txtEmployeeId.Text.Trim();
            if (!EmployeeValidator.IsValidId(input))
            {
                MessageBox.Show("Invalid Employee ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmployeeId.Clear();
                txtEmployeeId.Focus();
                return;
            }

            try
            {
                Employee employee = new Employee(Convert.ToInt32(txtEmployeeId.Text));
                employee.DeleteEmployee(employee);
                MessageBox.Show("Employee data has been deleted", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comBoxEmployeeSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comBoxEmployeeSearchBy.SelectedIndex)
            {
                case 0:
                    labelEmpSearch1.Text = "Enter Employee ID";
                    labelEmpSearch1.Visible = true;
                    labelEmpSearch2.Visible = false;
                    txtEmployeeSearch1.Visible = true;
                    txtEmployeeSearch2.Visible = false;
                    txtEmployeeSearch1.Clear();
                    txtEmployeeSearch1.Focus();
                    break;
                case 1:
                    labelEmpSearch1.Text = "Enter First Name";
                    labelEmpSearch2.Text = "Enter Last Name";
                    labelEmpSearch1.Visible = true;
                    labelEmpSearch2.Visible = true;
                    txtEmployeeSearch1.Visible = true;
                    txtEmployeeSearch2.Visible = true;
                    txtEmployeeSearch1.Clear();
                    txtEmployeeSearch1.Focus();
                    break;
                default:
                    break;
            }
        }

        private void btnSearchEmployee_Click(object sender, EventArgs e)
        {
            if(comBoxEmployeeSearchBy.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comBoxEmployeeSearchBy.Focus();
                return;
            }

            string input = "";
            Employee employee = new Employee();
            List<Employee> listEmployee = new List<Employee>();
            switch (comBoxEmployeeSearchBy.SelectedIndex)
            {
                case 0:
                    input = txtEmployeeSearch1.Text.Trim();
                    if (!EmployeeValidator.IsValidId(input))
                    {
                        MessageBox.Show("Invalid Employee ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEmployeeSearch1.Clear();
                        txtEmployeeSearch1.Focus();
                        return;
                    }
                    int employeeId = Convert.ToInt32(input);
                    employee = employee.SearchEmployee(employeeId);
                    DisplayInfoEmployee(employee);
                    break;
                case 1:
                    input = txtEmployeeSearch1.Text.Trim();
                    if (!EmployeeValidator.IsValidName(input))
                    {
                        MessageBox.Show("Invalid First Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEmployeeSearch1.Clear();
                        txtEmployeeSearch1.Focus();
                        return;
                    }
                    input = txtEmployeeSearch2.Text.Trim();
                    if (!EmployeeValidator.IsValidName(input))
                    {
                        MessageBox.Show("Invalid Last Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEmployeeSearch2.Clear();
                        txtEmployeeSearch2.Focus();
                        return;
                    }
                    listEmployee = employee.SearchEmployee(txtEmployeeSearch1.Text, txtEmployeeSearch2.Text);
                    DisplayInfoEmployee(listEmployee, listViewEmployee);
                    txtEmployeeSearch1.Clear();
                    txtEmployeeSearch2.Clear();
                    break;
                default:
                    break;
            }
        }

        private void DisplayInfoEmployee(Employee employee)
        {
            txtEmployeeId.Text = employee.EmployeeId.ToString();
            txtEmail.Text = employee.Email;
            txtFirstName.Text = employee.FirstName;
            txtLastName.Text = employee.LastName;
            comboBoxJobEmp.Text = employee.JobTitle;
            comboBoxStateIdEmp.Text = employee.StatusDescription;

        }

        private void DisplayInfoEmployee(List<Employee> listEmployee, ListView listEmpV )
        {
            listEmpV.Items.Clear();
            
            if(listEmployee.Count != 0)
            {
                foreach (Employee employee in listEmployee)
                {
                    ListViewItem item = new ListViewItem(employee.EmployeeId.ToString());
                    item.SubItems.Add(employee.FirstName);
                    item.SubItems.Add(employee.LastName);
                    item.SubItems.Add(employee.Email);
                    item.SubItems.Add(employee.JobTitle);
                    item.SubItems.Add(employee.StatusDescription);
                    listViewEmployee.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("No employee found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListEmployee_Click(object sender, EventArgs e)
        {
            listViewEmployee.Items.Clear();
            Employee employee = new Employee();
            List<Employee> listEmployee = employee.GetAllEmployee();
            DisplayInfoEmployee(listEmployee, listViewEmployee);
        }

        private bool ValidateUserInput()
        {
            string input = txtUserId.Text.Trim();
            if (!UserValidator.IsValidId(input))
            {
                MessageBox.Show("Invalid User ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserId.Clear();
                txtUserId.Focus();
                return false;
            }

            input = txtPassword.Text.Trim();
            if (!UserValidator.IsValidPassword(input))
            {
                MessageBox.Show("Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return false;
            }

            input = getStateIdUser().ToString();
            if (!UserValidator.IsValidId(input))
            {
                MessageBox.Show("Invalid Status ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxStateUser.Focus();;
                return false;
            }

            return true;    
        }


        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            if (!ValidateUserInput())
            {
                return;
            }

            try
            {
                int userId = Convert.ToInt32(txtUserId.Text);
                string password = txtPassword.Text;
                int statusId = getStateIdUser();

                UserAccount user = new UserAccount(userId, password, statusId);

                user.SaveUserAccount(user);
                MessageBox.Show("User data has been saved", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (!ValidateUserInput())
            {
                return;
            }

            try
            {
                UserAccount user = new UserAccount(Convert.ToInt32(txtUserId.Text), txtPassword.Text, selectedStatusId);
                user.UpdateUserAccount(user);
                MessageBox.Show("User data has been updated", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            string input = txtUserId.Text.Trim();
            if (!UserValidator.IsValidId(input))
            {
                MessageBox.Show("Invalid User ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserId.Clear();
                txtUserId.Focus();
                return;
            }

            try
            {
                UserAccount user = new UserAccount(Convert.ToInt32(txtUserId.Text));
                user.DeleteUserAccount(user);
                MessageBox.Show("User data has been deleted", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchUser_Click(object sender, EventArgs e)
        {
            string input = txtUserSearch.Text.Trim();
            if (!UserValidator.IsValidId(input))
            {
                MessageBox.Show("Invalid User ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserId.Clear();
                txtUserId.Focus();
                return;
            }

            UserAccount user = new UserAccount();
            user = user.SearchUserAccount(Convert.ToInt32(input));
            DisplayInfoUser(user);

        }

        private void DisplayInfoUser(UserAccount user)
        {
            txtUserId.Text = user.UserId.ToString();
            txtPassword.Text = user.Password;
            comboBoxStateUser.Text = user.StatusDescription;
        }

        private void DisplayInfoUser(List<UserAccount> listUser, ListView listUserV)
        {
            listUserV.Items.Clear();

            if (listUser.Count != 0)
            {
                foreach (UserAccount user in listUser)
                {
                    string pwd_fake = "********";
                    ListViewItem item = new ListViewItem(user.UserId.ToString());
                    item.SubItems.Add(pwd_fake);
                    item.SubItems.Add(user.DateCreated.ToString("yyyy-MM-dd"));
                    item.SubItems.Add(user.DateModified.ToString("yyyy-MM-dd"));
                    item.SubItems.Add(user.StatusDescription.ToString());
                    listUserV.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("No employee found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListAccounts_Click(object sender, EventArgs e)
        {
            listViewAccounts.Items.Clear();
            UserAccount user = new UserAccount();
            List<UserAccount> listUser = user.GetAllUserAccount();
            DisplayInfoUser(listUser, listViewAccounts);
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

        private void loadComboBoxJobs()
        {
            comboBoxStateIdEmp.Items.Clear();
            comboBoxStateUser.Items.Clear();
            Jobs job = new Jobs();
            jobsDictionary = job.GetAllJobsWithIds();

            foreach (var pair in jobsDictionary)
            {
                comboBoxJobEmp.Items.Add(pair.Key);
            }

        }

        private void loadComboBoxState()
        {
            // Initialize the stateDictionary 
            stateDictionary = new Dictionary<string, int>();

            // Load states for Employees
            State state = new State();
            //var stateDictionaryEmp = state.GetAllStateWithIds("Employee");
            //foreach (var pair in stateDictionaryEmp)
            //{
            //    comboBoxStateIdEmp.Items.Add(pair.Key);
            //    stateDictionary.Add(pair.Key, pair.Value); // Populate the stateDictionary
            //}
            stateDictionary = state.GetAllStateWithIds("Employee");

            foreach (var pair in stateDictionary)
            {
                comboBoxStateIdEmp.Items.Add(pair.Key);
            }

            userStateDictionary = state.GetAllStateWithIds("UserAccount");
            foreach (var pair in userStateDictionary)
            {
                comboBoxStateUser.Items.Add(pair.Key);
            }


        }

        private int getStateIdEmployee()
        {

            if (comboBoxStateIdEmp.SelectedItem != null)
            {
                string selectedDescription = comboBoxStateIdEmp.SelectedItem.ToString();
                // Use the description to get the ID from the employee state dictionary
                if (stateDictionary.TryGetValue(selectedDescription, out int statusId))
                {
                    return statusId; // Return the key which is the status ID
                }
                else
                {
                    MessageBox.Show("Selected state is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
            else
            {
                MessageBox.Show("Please select a state.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

        }

        private void comboBoxStateIdEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stateId = getStateIdEmployee();
            if (stateId == -1)
            {
                MessageBox.Show("Please select a state.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }




        private int getStateIdUser()
        {
            if (comboBoxStateUser.SelectedItem != null)
            {
                string selectedStateDescription = comboBoxStateUser.SelectedItem.ToString();
                if (userStateDictionary.TryGetValue(selectedStateDescription, out int statusId))
                {
                    selectedStatusId = statusId; // Save the status ID to the class member variable
                    return statusId; // This is the key corresponding to the value selected in the combo box
                }
                else
                {
                    MessageBox.Show("Selected state is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
            else
            {
                MessageBox.Show("Please select a state.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }

        }

        private void comboBoxStateUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stateId = getStateIdUser();
            if (stateId == -1)
            {
                MessageBox.Show("Please select a valid state for the user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int getJobIdEmployeeSelect()
        {
            if (comboBoxJobEmp.SelectedItem != null)
            {
                string selectedDescription = comboBoxJobEmp.SelectedItem.ToString();
                selectedJobId = jobsDictionary[selectedDescription];
                return selectedJobId;
            }
            else
            {
                return -1;
            }
            
        }

        private void comboBoxJobEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            getJobIdEmployeeSelect();
        }

        private void LoadUserName(int userId)
        {
            UserAccount user = new UserAccount();
            labelUserName.Text = user.GetUserNameUser(userId);
        }

        public int UserName { get; set; }

        private void pictureBoxViewProfile_Click(object sender, EventArgs e)
        {
            FormUserProfile formUserProfile = new FormUserProfile();
            formUserProfile.UserId = UserName;
            formUserProfile.Show();
        }

        private void btnViewProfile_Click(object sender, EventArgs e)
        {
            FormUserProfile formUserProfile = new FormUserProfile();
            formUserProfile.UserId = UserName;
            formUserProfile.Show();
        }
    }
}
