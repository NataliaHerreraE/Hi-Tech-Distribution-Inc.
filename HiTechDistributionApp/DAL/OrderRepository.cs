using HiTechDistributionApp.BLL.entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.DAL
{
    public class OrderRepository
    {
        public readonly HiTechDistributionDBContext dBContext;

        public OrderRepository()
        {
            dBContext = new HiTechDistributionDBContext();
        }

        public IEnumerable<Order> GetOrders() => dBContext.Orders.ToList();

        public void AddOrder(Order order)
        {
            if (order != null)
            {
                dBContext.Orders.Add(order);
                dBContext.SaveChanges();
            }
            else
            {
                throw new Exception("Order cannot be null");
            }
        }

        public void UpdateOrder(Order order)
        {
            if (order != null)
            {
                Order orderToUpdate = dBContext.Orders.Find(order.OrderID);
                if (orderToUpdate != null)
                {
                    orderToUpdate.OrderDate = order.OrderDate;
                    orderToUpdate.CustomerID = order.CustomerID;
                    orderToUpdate.EmployeeID = order.EmployeeID;
                    orderToUpdate.OrderDate = order.OrderDate;
                    orderToUpdate.OrderType = order.OrderType;
                    orderToUpdate.StatusId = order.StatusId;
                    dBContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Order not found");
                }
            }
            else
            {
                throw new Exception("Order cannot be null");
            }
        }

        

        public void CancelOrder(int orderId)
        {
            Order orderToCancel = dBContext.Orders.Find(orderId);
            if (orderToCancel != null)
            {
                // Assume that 'canceledStatusId' is the ID from your 'Status' table that represents "Canceled".
                int canceledStatusId = dBContext.Status
                                                 .Where(s => s.State == "Canceled")
                                                 .Select(s => s.StatusId)
                                                 .FirstOrDefault(); // Make sure you handle the case where this might be 0 if "Canceled" doesn't exist.

                if (canceledStatusId == 0)
                {
                    throw new Exception("Canceled status not found in Status table.");
                }

                if (orderToCancel != null)
                {
                    orderToCancel.StatusId = canceledStatusId;  // Set the order's status to "Canceled"
                    dBContext.SaveChanges();                   // Save changes to the database
                }
                else
                {
                    throw new Exception("Order not found");    // Handle the case where the order doesn't exist
                }
            }
            else
            {
                throw new Exception("Order not found");
            }
        }

        public Order GetOrderById(int orderId) => dBContext.Orders.Find(orderId);
        public IEnumerable<Order> GetOrderByCustomerId(int customerId) => dBContext.Orders.Where(o => o.CustomerID == customerId).ToList();
        public IEnumerable<Order> GetOrderByEmployeeId(int employeeId) => dBContext.Orders.Where(o => o.EmployeeID == employeeId).ToList();
        //public IEnumerable<Order> GetOrderByStatusId(int statusId) => dBContext.Orders.Where(o => o.StatusId == statusId).ToList();
        public IEnumerable<Order> GetOrdersByState(string state)
        {
            return dBContext.Orders
                .Join(
                    dBContext.Status,
                    order => order.StatusId,
                    status => status.StatusId,
                    (order, status) => new { Order = order, Status = status }
                )
                .Where(os => os.Status.State.Contains(state))
                .Select(os => os.Order)
                .ToList();
        }

        public bool IsBookWithStatusOpenExists(int bookId)
        {
           return dBContext.OrdersDetails  
                              .Any(od => od.BookID == bookId && od.Order.StatusId == 3);
           
        }


    }
}
