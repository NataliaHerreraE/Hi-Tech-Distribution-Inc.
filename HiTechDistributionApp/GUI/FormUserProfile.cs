using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HiTechDistributionApp.BLL;
using HiTechDistributionApp.VALIDATION;

namespace HiTechDistributionApp.GUI
{
    public partial class FormUserProfile : Form
    {
        public FormUserProfile()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxJob_TextChanged(object sender, EventArgs e)
        {

        }

        public int UserId { get; set; }

        private void FormUserProfile_Load(object sender, EventArgs e)
        {

            int employeeId = UserId;
            BLL.Employee employee = new Employee();
            employee = employee.SearchEmployee(employeeId);
            DisplayInfoEmployee(employee);

        }

        private void DisplayInfoEmployee(Employee employee)
        {
            txtEmployeeId.Text = employee.EmployeeId.ToString();
            txtEmail.Text = employee.Email;
            txtFirstName.Text = employee.FirstName;
            txtLastName.Text = employee.LastName;
            textBoxJob.Text = employee.JobTitle;
            textBoxStatus.Text = employee.StatusDescription;

        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (!ValidateUserInput())
            {
                return;
            }

            string inputPassword = txtPasswordOLD.Text.Trim();
            if (!UserValidator.IsValidPassword(inputPassword))
            {
                MessageBox.Show("length of the password and minimun is 8 with at least one uppercase", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            int idUser = Convert.ToInt32(txtEmployeeId.Text.Trim());
            BLL.UserAccount account = new BLL.UserAccount();
           
            if (account.LoginUserAccount(idUser, inputPassword))
            {
                if(txtPassword.Text == txtPasswordNEW.Text)
                {
                    try
                    {
                        UserAccount user = new UserAccount(Convert.ToInt32(txtEmployeeId.Text), txtPassword.Text);
                        user.UpdateUserAccountByUser(user);
                        MessageBox.Show("User data has been updated", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else
                {
                    MessageBox.Show("Passwords do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
                
            } else
            {
                MessageBox.Show("Invalid Old Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private bool ValidateUserInput()
        {

            string input = txtPassword.Text.Trim();
            if (!UserValidator.IsValidPassword(input))
            {
                MessageBox.Show("Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return false;
            }

            return true;
        }
    }
}
