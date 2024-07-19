using HiTechDistributionApp.BLL;
using HiTechDistributionApp.BLL.entity;
using HiTechDistributionApp.DAL;
using HiTechDistributionApp.VALIDATION;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HiTechDistributionApp.GUI
{
    public partial class FormOrderClerks : Form
    {
        private readonly OrderController orderController;
        private readonly OrdersDetailController ordersDetailController;
        private readonly CustomerController customerController;
        private readonly StatusController statusController;
        private readonly EmployeeController employeeController;
        private readonly BookController bookController;
        private Dictionary<string, int> statusDictionary;
        private bool isStatusLoaded = false;
        private Dictionary<string, int> employeeDictionary;
        private bool isEmployeeLoaded = false;
        private Dictionary<string, int> CustomerDictionary;
        private bool isCustomerLoaded = false;
        private Dictionary<string, int> BookDictionary;
        private bool isBookLoaded = false;

        public FormOrderClerks()
        {
            orderController = new OrderController();
            ordersDetailController = new OrdersDetailController();
            customerController = new CustomerController();
            statusController = new StatusController();
            employeeController = new EmployeeController();
            bookController = new BookController();
            InitializeComponent();
            txtOrderSearch1.Visible = false;
            labelOrderSearch1.Visible = false;
            txtOrderDetailSearch.Visible = false;
            labelOrderDetailSearch.Visible = false;
            txtSearchByCustomer.Visible = false;
            labelSearchByCustomer.Visible = false;

        }
        FormUserProfile formUserProfile = new FormUserProfile();

        private void FormOrderClerks_Load(object sender, EventArgs e)
        {
            isStatusLoaded = false;
            isStatusLoaded = false;
            isCustomerLoaded = false;
            LoadStatuses();
            LoadEmployees();
            LoadCustomers();
            LoadOrderId();
            LoadBooksId();
            LoadUserName(UserName);
            formUserProfile.UserId = UserName;
            isStatusLoaded = statusDictionary != null && statusDictionary.Any();
        }

        //order form

        private bool ValidateOrder(int typeOperation)
        {
            string orderId = txtOrderId.Text.Trim();
            if (typeOperation != 1 && OrderValidator.IsCloseOrder(orderId))
            {
                MessageBox.Show("Closed order (Completed or Canceled), please choose another order to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (comboBoxEmployeeId.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBoxCustomerId.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string input = txtOrderDate.Text;
            if (!OrderValidator.isValidDate(input))
            {
                MessageBox.Show("Please enter the Order Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            input = comboBoxOrderType.Text;
            if (!OrderValidator.isValidOrderType(input))
            {
                MessageBox.Show("Please enter the Order Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            input = comboBoxStatusId.Text;
            if (!OrderValidator.isValidStatus(input))
            {
                MessageBox.Show("Please enter the Status", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            string selectedStatus = comboBoxStatusId.Text;
            string selectedEmployee = comboBoxEmployeeId.SelectedItem.ToString();
            string selectedCustomer = comboBoxCustomerId.SelectedItem.ToString();

            if (ValidateOrder(1))
            {

                if (statusDictionary.TryGetValue(selectedStatus, out int statusId) &&
                    employeeDictionary.TryGetValue(selectedEmployee, out int employeeId) &&
                    CustomerDictionary.TryGetValue(selectedCustomer, out int customerId))
                {
                    
                    Order order = new Order()
                    {
                        CustomerID = customerId,
                        EmployeeID = employeeId,
                        OrderDate = Convert.ToDateTime(txtOrderDate.Text).Date,
                        OrderType = comboBoxOrderType.Text,
                        StatusId = statusId
                    };

                    // Add the order using the orderController.
                    try
                    {
                        orderController.AddOrder(order);

                    } catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while adding the order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    MessageBox.Show("Order has been added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOrderId();
                }
                else
                {
                    // Handle the case where the status is not found in the dictionary.
                    MessageBox.Show("The selected status is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid order input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            if (ValidateOrder(0))
            {
                orderController.UpdateOrder(new Order()
                {
                    OrderID = int.Parse(txtOrderId.Text),
                    CustomerID = CustomerDictionary[comboBoxCustomerId.SelectedItem.ToString()],
                    EmployeeID = employeeDictionary[comboBoxEmployeeId.SelectedItem.ToString()],
                    OrderDate = Convert.ToDateTime(txtOrderDate.Text).Date,
                    OrderType = comboBoxOrderType.Text,
                    StatusId = statusDictionary[comboBoxStatusId.SelectedItem.ToString()]
                });
                MessageBox.Show("Order has been updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Invalid order Operation. Please check your inputs and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to cancel this order? ", "Cancel Order ", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString() == "Yes")
            {
                string input = txtOrderId.Text;
                if (!OrderValidator.IsValidId(input))
                {
                    MessageBox.Show("Please enter the Order ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                orderController.CancelOrder(Convert.ToInt32(txtOrderId.Text));
                MessageBox.Show("Order has been canceled successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            else
            {
                MessageBox.Show("Error cancel the order", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListAllOrders_Click(object sender, EventArgs e)
        {
            if (orderController.GetOrders().Count() > 0)
            {
                listViewOrders.Items.Clear();
                foreach (var order in orderController.GetOrders())
                {
                    string customerName = CustomerDictionary.FirstOrDefault(c => c.Value == order.CustomerID).Key;
                    string employeeName = employeeDictionary.FirstOrDefault(emp => emp.Value == order.EmployeeID).Key;
                    string statusName = statusDictionary.FirstOrDefault(s => s.Value == order.StatusId).Key;

                    ListViewItem item = new ListViewItem(order.OrderID.ToString());
                    item.SubItems.Add(customerName ?? $"Unknown ({order.CustomerID})");
                    item.SubItems.Add(employeeName ?? $"Unknown ({order.EmployeeID})");
                    item.SubItems.Add(order.OrderDate.ToString("yyyy-MM-dd"));
                    item.SubItems.Add(order.OrderType);
                    item.SubItems.Add(statusName ?? $"Unknown ({order.StatusId})");
                    listViewOrders.Items.Add(item);


                }
            }
            else
            {
                MessageBox.Show("No orders found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchOrder_Click(object sender, EventArgs e)
        {
            if (comBoxOrderSearchBy.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
           
            try
            {
                IEnumerable<Order> results = null;
                Order order = null;
                string input = txtOrderSearch1.Text;

                if (comBoxOrderSearchBy.SelectedIndex == 0 && int.TryParse(input, out int orderId))
                {
                    order = orderController.GetOrderById(orderId);
                }
                else if (comBoxOrderSearchBy.SelectedIndex == 1 && int.TryParse(input, out int customerId))
                {
                    results = orderController.GetOrderByCustomerId(customerId);
                }
                else if (comBoxOrderSearchBy.SelectedIndex == 2 && int.TryParse(input, out int employeeId))
                {
                    results = orderController.GetOrderByEmployeeId(employeeId);
                }
                else if (comBoxOrderSearchBy.SelectedIndex == 3)
                {
                    results = orderController.GetOrderByStatusId(input);
                }

                if (order != null)
                {
                    txtOrderId.Text = order.OrderID.ToString();
                    comboBoxCustomerId.SelectedItem = CustomerDictionary.FirstOrDefault(x => x.Value == order.CustomerID).Key;
                    comboBoxEmployeeId.SelectedItem = employeeDictionary.FirstOrDefault(x => x.Value == order.EmployeeID).Key;
                    txtOrderDate.Text = order.OrderDate.ToString("yyyy-MM-dd");
                    comboBoxOrderType.SelectedItem = order.OrderType;
                    comboBoxStatusId.SelectedItem = statusDictionary.FirstOrDefault(x => x.Value == order.StatusId).Key;
                } 
                else if (results != null && results.Any())
                    {
                    PopulateOrderListView(results);
                }
                else
                {
                    MessageBox.Show("Order not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while searching for the order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateOrderListView(IEnumerable<Order> order)
        {
            listViewOrders.Items.Clear();
            foreach (var detail in order) // Use the passed-in collection
            {
                ListViewItem item = new ListViewItem(detail.OrderID.ToString());
                item.SubItems.Add(CustomerDictionary.FirstOrDefault(x => x.Value == detail.CustomerID).Key);
                item.SubItems.Add(employeeDictionary.FirstOrDefault(x => x.Value == detail.EmployeeID).Key);
                item.SubItems.Add(detail.OrderDate.ToString());
                item.SubItems.Add(detail.OrderType);
                item.SubItems.Add(statusDictionary.FirstOrDefault(x => x.Value == detail.StatusId).Key);
                listViewOrders.Items.Add(item);
            }
        }


        private void LoadStatuses()
        {
            statusDictionary = statusController.GetStatusDictionary();
            comboBoxStatusId.Items.Clear();

            if (statusDictionary != null && statusDictionary.Any())
            {
                foreach (var status in statusDictionary)
                {
                    comboBoxStatusId.Items.Add(status.Key);
                }
            }
            else
            {
                MessageBox.Show("Statuses could not be loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadEmployees()
        {
            employeeDictionary = employeeController.GetEmployeeDictionary();
            comboBoxEmployeeId.Items.Clear();

            if (employeeDictionary != null && employeeDictionary.Any())
            {
                foreach (var employee in employeeDictionary)
                {
                    comboBoxEmployeeId.Items.Add(employee.Key);
                }
            }
            else
            {
                MessageBox.Show("Employees could not be loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCustomers()
        {
            CustomerDictionary = customerController.GetCustomerDictionary();
            comboBoxCustomerId.Items.Clear();

            if (CustomerDictionary != null && CustomerDictionary.Any())
            {
                foreach (var customer in CustomerDictionary)
                {
                    comboBoxCustomerId.Items.Add(customer.Key);
                }
            }
            else
            {
                MessageBox.Show("Customers could not be loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrderId()
        {
            var orders = orderController.GetOrders();
            comboBoxOrderId.Items.Clear();

            if (orders != null && orders.Any())
            {
                foreach (var order in orders)
                {
                    comboBoxOrderId.Items.Add(order.OrderID);
                }
            }
            else
            {
                MessageBox.Show("No orders found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBooksId()
        {
            var bookDict = bookController.GetBookDictionary();

            if (bookDict.Count > 0)
            {
                comboBoxBookId.DataSource = new BindingSource(bookDict, null);
                comboBoxBookId.DisplayMember = "Key";
                comboBoxBookId.ValueMember = "Value";
                comboBoxBookId.SelectedIndex = -1; // Add this line
            }
            else
            {
                MessageBox.Show("No books found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void comboBoxStatusId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isStatusLoaded)
            {
                LoadStatuses();
            }
        }

        //order detail form

        private bool ValidateOrderDetail()
        {
            
            
            if (comboBoxOrderId.SelectedItem == null)
            {
                MessageBox.Show("Please select an Order ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                string selectedOrderId = comboBoxOrderId.SelectedItem.ToString();
                if (OrderValidator.IsCloseOrder(selectedOrderId))
                {
                    MessageBox.Show("Closed order (Completed or Canceled), please choose another order to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }

            
            if (comboBoxBookId.SelectedItem == null)
            {
                MessageBox.Show("Please select a Book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string input = txtQuantity.Text.Trim();
            if (!OrderValidator.isValidQuantity(input))
            {
                MessageBox.Show("Please enter the Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            input = txtUnitPrice.Text;
            if (!OrderValidator.isValidPrice(input))
            {
                MessageBox.Show("Please enter the Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            input = txtTotalPrice.Text;
            if (!OrderValidator.isValidPrice(input))
            {
                MessageBox.Show("Please enter the Total Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidateOrderDetailbyDelete()
        {

            if (comboBoxOrderId.SelectedItem == null)
            {
                MessageBox.Show("Please select an Order ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                string selectedOrderId = comboBoxOrderId.SelectedItem.ToString();
                if (OrderValidator.IsCloseOrder(selectedOrderId))
                {
                    MessageBox.Show("Closed order (Completed or Canceled), please choose another order to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }


            if (comboBoxBookId.SelectedItem == null)
            {
                MessageBox.Show("Please select a Book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnAddOrderDetails_Click(object sender, EventArgs e)
        {

            if (ValidateOrderDetail())
            {

                string orderId2 = comboBoxOrderId.SelectedItem?.ToString();
                string itemSequencial2 = txtItemSequencial.Text;

                if (!OrderValidator.isValidItemSequencial(itemSequencial2, orderId2, ordersDetailController.GetOrdersDetails()))
                {
                    MessageBox.Show("Item Sequencial is not valid for the selected Order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int orderId = int.Parse(comboBoxOrderId.SelectedItem.ToString());
                int bookId = (int)comboBoxBookId.SelectedValue;
                int itemSequencial = int.Parse(txtItemSequencial.Text); 
                int quantity = int.Parse(txtQuantity.Text);
                string unitPriceText = txtUnitPrice.Text.Replace("$", "").Trim();
                string totalPriceText = txtTotalPrice.Text.Replace("$", "").Trim();
                bool unitPriceResult = decimal.TryParse(unitPriceText, out decimal unitPrice);
                bool totalPriceResult = decimal.TryParse(totalPriceText, out decimal totalPrice);


                if (unitPriceResult && totalPriceResult)
                {
                    OrdersDetail orderDetail = new OrdersDetail()
                    {
                        ItemSequencial = int.Parse(txtItemSequencial.Text),
                        OrderID = int.Parse(comboBoxOrderId.SelectedItem.ToString()),
                        BookID = int.Parse(comboBoxBookId.SelectedValue.ToString()),
                        Quantity = int.Parse(txtQuantity.Text),
                        CurrentUnitPrice = unitPrice,
                        PriceTotal = totalPrice
                    };

                    try
                    {
                        ordersDetailController.AddOrdersDetail(orderDetail);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    bookController.UpdateQuantity(bookId, -quantity);
                    MessageBox.Show("Order Detail has been added successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please enter valid numbers for Unit Price and Total Price.");
                }
            }
            else
            {
                MessageBox.Show("Invalid order detail input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateOrderDetails_Click(object sender, EventArgs e)
        {

            if (ValidateOrderDetail())
            {
                string itemSequencial = txtItemSequencial.Text;
                if (!OrderValidator.IsValidId(itemSequencial))
                {
                    MessageBox.Show("Item Sequencial must be a valid numeric ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int orderId = int.Parse(comboBoxOrderId.SelectedItem.ToString());
                int bookId = (int)comboBoxBookId.SelectedValue;
                int quantity = int.Parse(txtQuantity.Text);
                string unitPriceText = txtUnitPrice.Text.Replace("$", "").Trim();
                string totalPriceText = txtTotalPrice.Text.Replace("$", "").Trim();

                if (decimal.TryParse(unitPriceText, out decimal unitPrice) && decimal.TryParse(totalPriceText, out decimal totalPrice))
                {
                    OrdersDetail orderDetail = new OrdersDetail()
                    {
                        ItemSequencial = int.Parse(itemSequencial),
                        OrderID = orderId,
                        BookID = bookId,
                        Quantity = quantity,
                        CurrentUnitPrice = unitPrice,
                        PriceTotal = totalPrice
                    };

                    var orderDetailsOld = ordersDetailController.SearchOrderDetailByBookID(bookId);
                    int quantityOld = orderDetailsOld.Sum(od => od.Quantity);
                    int quantityDifference = quantityOld - quantity;
                    try
                    {  
                        ordersDetailController.UpdateOrdersDetail(orderDetail);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bookController.UpdateQuantity(bookId, quantityDifference);
                    MessageBox.Show("Order Detail has been updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please enter valid numbers for Unit Price and Total Price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid order detail input. Please check your inputs and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchOrderDetails_Click(object sender, EventArgs e)
        {
            if (comBoxOrderDetailSearchBy.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a search criterion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                IEnumerable<OrdersDetail> results = null;

                switch (comBoxOrderDetailSearchBy.SelectedIndex)
                {
                    case 0: 
                        if (string.IsNullOrWhiteSpace(txtOrderDetailSearch.Text))
                        {
                            MessageBox.Show("Please write an Order ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (int.TryParse(txtOrderDetailSearch.Text.Trim(), out int orderId))
                        {
                            results = ordersDetailController.SearchOrderDetailById(orderId);
                        }
                        else
                        {
                            MessageBox.Show("Invalid Order ID format. Please enter a numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                    case 1: 
                        if (string.IsNullOrWhiteSpace(txtOrderDetailSearch.Text))
                        {
                            MessageBox.Show("Please write a Book ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (int.TryParse(txtOrderDetailSearch.Text.Trim(), out int bookId))
                        {
                            results = ordersDetailController.SearchOrderDetailByBookID(bookId);
                        }
                        else
                        {
                            MessageBox.Show("Invalid Book ID format. Please enter a numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                }

               
                if (results != null && results.Any())
                {
                    PopulateOrderDetailsListView(results);
                }
                else
                {
                    MessageBox.Show("No order details found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void PopulateOrderDetailsListView(IEnumerable<OrdersDetail> orderDetails)
        {
            listViewOrderDetails.Items.Clear();
            foreach (var detail in orderDetails) // Use the passed-in collection
            {
                ListViewItem item = new ListViewItem(detail.OrderID.ToString());
                item.SubItems.Add(detail.ItemSequencial.ToString());
                string bookTitle = bookController.GetBookTitleById(Convert.ToInt32(detail.BookID));
                item.SubItems.Add(bookTitle ?? "Unknown Book");
                item.SubItems.Add(detail.Quantity.ToString());
                item.SubItems.Add(detail.CurrentUnitPrice.ToString());
                item.SubItems.Add(detail.PriceTotal.ToString());
                listViewOrderDetails.Items.Add(item);
            }
        } 
        
        

        private void btnListAllOrderDetails_Click(object sender, EventArgs e)
        {
            if (ordersDetailController.GetOrdersDetails().Count() > 0)
            {
                listViewOrderDetails.Items.Clear();
                foreach (var orderDetail in ordersDetailController.GetOrdersDetails())
                {
                    ListViewItem item = new ListViewItem(orderDetail.OrderID.ToString());
                    item.SubItems.Add(orderDetail.ItemSequencial.ToString());
                    string bookTitle = bookController.GetBookTitleById(Convert.ToInt32(orderDetail.BookID));
                    item.SubItems.Add(bookTitle ?? "Unknown Book");
                    item.SubItems.Add(orderDetail.Quantity.ToString());
                    item.SubItems.Add(orderDetail.CurrentUnitPrice.ToString());
                    item.SubItems.Add(orderDetail.PriceTotal.ToString());
                    listViewOrderDetails.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("No order details found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //customer form

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            listViewCustomers.Items.Clear();

            if (comBoxSearchByCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a search criteria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comBoxSearchByCustomer.Focus();
                return;
            }

            if (comBoxSearchByCustomer.SelectedIndex == 0) // Search by Customer ID
            {
                if (!CustomerValidation.IsValidId(txtSearchByCustomer.Text))
                {
                    MessageBox.Show("Invalid Customer ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSearchByCustomer.Focus();
                    return;
                }

                if (int.TryParse(txtSearchByCustomer.Text, out int customerId))
                {
                    var customerById = customerController.SearchCustomerById(customerId);
                    if (customerById != null)
                    {
                        ListViewItem item = new ListViewItem(customerById.CustomerID.ToString());
                        item.SubItems.Add(customerById.CustomerName);
                        item.SubItems.Add(customerById.Street);
                        item.SubItems.Add(customerById.PhoneNumber);
                        item.SubItems.Add(customerById.City);
                        item.SubItems.Add(customerById.PostalCode);
                        item.SubItems.Add(customerById.FaxNumber);
                        item.SubItems.Add(customerById.CreditLimit.ToString());
                        listViewCustomers.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Customer not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid Customer ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comBoxSearchByCustomer.SelectedIndex == 1) // Search by Customer Name
            {
                if (!CustomerValidation.IsValidName(txtSearchByCustomer.Text))
                {
                    MessageBox.Show("Invalid Customer Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSearchByCustomer.Focus();
                    return;
                }

                var customerByName = customerController.SearchCustomerByName(txtSearchByCustomer.Text);
                if (customerByName != null)
                {
                    ListViewItem item = new ListViewItem(customerByName.CustomerID.ToString());
                    item.SubItems.Add(customerByName.CustomerName);
                    item.SubItems.Add(customerByName.Street);
                    item.SubItems.Add(customerByName.PhoneNumber);
                    item.SubItems.Add(customerByName.City);
                    item.SubItems.Add(customerByName.PostalCode);
                    item.SubItems.Add(customerByName.FaxNumber);
                    item.SubItems.Add(customerByName.CreditLimit.ToString());
                    listViewCustomers.Items.Add(item);
                }
                else
                {
                    MessageBox.Show("Customer not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnListAllCustomer_Click(object sender, EventArgs e)
        {
            if (customerController.GetCustomers().Count() > 0)
            {
                listViewCustomers.Items.Clear();
                foreach (var customer in customerController.GetCustomers())
                {
                    ListViewItem item = new ListViewItem(customer.CustomerID.ToString());
                    item.SubItems.Add(customer.CustomerName);
                    item.SubItems.Add(customer.Street);
                    item.SubItems.Add(customer.PhoneNumber);
                    item.SubItems.Add(customer.City);
                    item.SubItems.Add(customer.PostalCode);
                    item.SubItems.Add(customer.FaxNumber);
                    item.SubItems.Add(customer.CreditLimit.ToString());
                    listViewCustomers.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("No customers found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void comBoxSearchByCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comBoxSearchByCustomer.SelectedIndex)
            {
                case 0:
                    labelSearchByCustomer.Text = "Enter Customer ID";
                    txtSearchByCustomer.Visible = true;
                    labelSearchByCustomer.Visible = true;
                    txtSearchByCustomer.Clear();
                    txtSearchByCustomer.Focus();
                    break;
                case 1:
                    labelSearchByCustomer.Text = "Enter Customer Name";
                    txtSearchByCustomer.Visible = true;
                    labelSearchByCustomer.Visible = true;
                    txtSearchByCustomer.Clear();
                    txtSearchByCustomer.Focus();
                   
                    break;
                default:
                    break;
            }
        }

        private void comBoxOrderDetailSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comBoxOrderDetailSearchBy.SelectedIndex)
            {
                case 0:
                    labelOrderDetailSearch.Text = "Enter Order ID";
                    labelOrderDetailSearch.Visible = true;
                    txtOrderDetailSearch.Visible = true;
                    txtOrderDetailSearch.Clear();
                    txtOrderDetailSearch.Focus();
                    break;

                case 1:
                    labelOrderDetailSearch.Text = "Enter Book ID";
                    labelOrderDetailSearch.Visible = true;
                    txtOrderDetailSearch.Visible = true;
                    txtOrderDetailSearch.Clear();
                    txtOrderDetailSearch.Focus();
                    break;
                default:
                    break;
            }
        }

        private void comBoxOrderSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comBoxOrderSearchBy.SelectedIndex)
            {
                case 0:
                    labelOrderSearch1.Text = "Enter Order ID";
                    txtOrderSearch1.Visible = true;
                    labelOrderSearch1.Visible = true;
                    txtOrderSearch1.Clear();
                    txtOrderSearch1.Focus();
                    break;
                case 1:
                    labelOrderSearch1.Text = "Enter Customer ID";
                    txtOrderSearch1.Visible = true;
                    labelOrderSearch1.Visible = true;
                    txtOrderSearch1.Clear();
                    txtOrderSearch1.Focus();
                    break;
                case 2:
                    labelOrderSearch1.Text = "Enter Employee ID";
                    txtOrderSearch1.Visible = true;
                    labelOrderSearch1.Visible = true;
                    txtOrderSearch1.Clear();
                    txtOrderSearch1.Focus();
                    break;
                case 3:
                    labelOrderSearch1.Text = "Enter Status ID";
                    txtOrderSearch1.Visible = true;
                    labelOrderSearch1.Visible = true;
                    txtOrderSearch1.Clear();
                    txtOrderSearch1.Focus();
                    break;
                default:
                    break;
            }
        }

        private void comboBoxBookId_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            if (comboBoxBookId.SelectedItem != null)
            {
                string selectedBookTitle = comboBoxBookId.GetItemText(comboBoxBookId.SelectedItem);
                BLL.entity.Book selectedBook = bookController.GetBooks()
                    .FirstOrDefault(b => $"{b.Title} ({b.BookID})" == selectedBookTitle);

                if (selectedBook != null)
                {
                    txtUnitPrice.Text = selectedBook.UnitPrice.ToString("C2");
                    CalculateTotalPrice();
                }
            }
        }

        private void CalculateTotalPrice()
        {
            if (OrderValidator.isValidQuantity(txtQuantity.Text) &&
                OrderValidator.isValidPrice(txtUnitPrice.Text))
            {
                int quantity = int.Parse(txtQuantity.Text);
                decimal unitPrice = decimal.Parse(Regex.Replace(txtUnitPrice.Text, @"[^\d\.]", ""));
                decimal totalPrice = quantity * unitPrice;
                txtTotalPrice.Text = totalPrice.ToString("C2");
            }
            else
            {
                txtTotalPrice.Text = "N/A";
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void comboBoxOrderId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadOrderId();
        }

        private void comboBoxCustomerId_SelectedIndexChanged(object sender, EventArgs e)
        {
           // LoadCustomers();
        }

        private void comboBoxEmployeeId_SelectedIndexChanged(object sender, EventArgs e)
        {
           // LoadEmployees();
        }

        private void LoadUserName(int userId)
        {
            BLL.UserAccount user = new BLL.UserAccount();
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

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (ValidateOrderDetailbyDelete())
            {
                string itemSequencial = txtItemSequencial.Text;
                if (!OrderValidator.IsValidId(itemSequencial))
                {
                    MessageBox.Show("Item Sequencial must be a valid numeric ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int orderId = int.Parse(comboBoxOrderId.SelectedItem.ToString());
                int bookId = (int)comboBoxBookId.SelectedValue;
               
                OrdersDetail orderDetail = new OrdersDetail()
                {
                    ItemSequencial = int.Parse(itemSequencial),
                    OrderID = orderId,
                    BookID = bookId
                };


                //int itemSequencialInt = int.Parse(itemSequencial);
                //var orderDetailsOld = ordersDetailController.SearchOrderDetailByOrderIdAndItemSequencial(orderId,itemSequencialInt);
                //if (!orderDetailsOld.Any())
                //{
                //    MessageBox.Show("No order details found for the provided book ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}
                //int quantityOld = orderDetailsOld.Sum(od => od.Quantity);
                int itemSequencialInt = int.Parse(itemSequencial);
                OrdersDetail orderDetailOld = ordersDetailController.SearchOrderDetailByOrderIdAndItemSequencial(orderId, itemSequencialInt);
                if (orderDetailOld == null)
                {
                    MessageBox.Show("No order detail found for the provided order ID and item sequencial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int bookIdOld = (int)orderDetailOld.BookID;
                int quantityOld = orderDetailOld.Quantity;

                try
                {
                    ordersDetailController.DeleteOrdersDetail(orderDetail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bookController.UpdateQuantity(bookIdOld, quantityOld);
                MessageBox.Show("Order Detail has been deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            else
            {
                MessageBox.Show("Invalid order detail input. Please check your inputs and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
