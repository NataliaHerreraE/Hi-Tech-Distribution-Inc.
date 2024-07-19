using System;
using HiTechDistributionApp.BLL;
using HiTechDistributionApp.VALIDATION;
using HiTechDistributionApp.GUI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace HiTechDistributionApp.GUI
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit this app? ", "Exit ", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                this.Close();
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string inputUserId = txtUserId.Text.Trim();
            if (!UserValidator.IsValidId(inputUserId))
            {
                MessageBox.Show("Invalid User ID, must be a number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserId.Clear();
                txtUserId.Focus();
                return;
            }

            string inputPassword = txtPassword.Text.Trim();
            if (!UserValidator.IsValidPassword(inputPassword))
            {
                MessageBox.Show("length of the password and minimun is 8 with at least one uppercase", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            int idUser = Convert.ToInt32(txtUserId.Text.Trim());
            BLL.UserAccount account = new BLL.UserAccount();
            BLL.Employee employee = new BLL.Employee();

            if (account.LoginUserAccount(idUser, inputPassword))
            {
                this.Hide();

                String jobTitle = employee.GetJobTitle(idUser);

                //display a message box to welcome 
                MessageBox.Show("Welcome " + jobTitle + " " + "!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                switch (jobTitle)
                {
                    case "MIS Manager":
                        FormMISManager formMISManager = new FormMISManager();
                        formMISManager.UserName = idUser;
                        formMISManager.Show();
                        break;
                    case "Sales Manager":
                        FormSalesManager salesManager = new FormSalesManager();
                        salesManager.UserName = idUser;
                        salesManager.Show();
                        break;
                    case "Order Clerk":
                        FormOrderClerks orderClerks = new FormOrderClerks();
                        orderClerks.UserName = idUser;
                        orderClerks.Show();
                        break;
                    case "Inventory Controller":
                        FormInventoryController inventoryController = new FormInventoryController();
                        inventoryController.UserName = idUser;
                        inventoryController.Show();
                        break;
                    default:
                        MessageBox.Show("Your position does not allow access to the program.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                        
                }

            }
            else
            {
                MessageBox.Show("Invalid User ID or Password, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserId.Clear();
                txtPassword.Clear();
                txtUserId.Focus();
            };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit this app? ", "Exit ", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                this.Close();
                Application.Exit();
            }
        }
    }
}
