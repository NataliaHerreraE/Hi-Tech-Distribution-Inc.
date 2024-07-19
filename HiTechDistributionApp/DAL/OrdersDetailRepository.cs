using HiTechDistributionApp.BLL.entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.DAL
{
    public class OrdersDetailRepository
    {
        private readonly HiTechDistributionDBContext dbContext;

        public OrdersDetailRepository()
        {
            dbContext = new HiTechDistributionDBContext();
        }

        public IEnumerable<OrdersDetail> GetOrdersDetails() => dbContext.OrdersDetails.ToList();

        public void AddOrdersDetail(OrdersDetail ordersDetail)
        {
            if (ordersDetail != null)
            {
                dbContext.OrdersDetails.Add(ordersDetail);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Error: OrdersDetail object is null.");
            }
        }

        public void UpdateOrdersDetail(OrdersDetail ordersDetail)
        {
            if (ordersDetail != null)
            {
                OrdersDetail ordersDetailToUpdate = dbContext.OrdersDetails.Find(ordersDetail.OrderID, ordersDetail.ItemSequencial);
                if (ordersDetailToUpdate != null)
                {
                    ordersDetailToUpdate.ItemSequencial = ordersDetail.ItemSequencial;
                    ordersDetailToUpdate.BookID = ordersDetail.BookID;
                    ordersDetailToUpdate.Quantity = ordersDetail.Quantity;
                    ordersDetailToUpdate.CurrentUnitPrice = ordersDetail.CurrentUnitPrice;
                    ordersDetailToUpdate.PriceTotal = ordersDetail.PriceTotal;
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Error: OrdersDetail object is null.");
                }
            }
        }

        public void DeleteOrdersDetail(OrdersDetail ordersDetail)
        {
            if (ordersDetail != null)
            {
                OrdersDetail ordersDetailToDelete = dbContext.OrdersDetails.Find(ordersDetail.OrderID, ordersDetail.ItemSequencial);
                if (ordersDetailToDelete != null)
                {
                    dbContext.OrdersDetails.Remove(ordersDetailToDelete);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Error: OrdersDetail object is null.");
                }
            }
        }

        //public OrdersDetail SearchOrderDetailById(int orderID) => dbContext.OrdersDetails.Find(orderID);

        public OrdersDetail SearchOrderDetailByItemSequencial(int itemSequencial) => dbContext.OrdersDetails.Find(itemSequencial);

        //public OrdersDetail SearchOrderDetailByBookID(int bookID) => dbContext.OrdersDetails.Find(bookID);

        public bool Exists(int itemSequential, int orderId)
        {
            return dbContext.OrdersDetails.Any(od => od.ItemSequencial == itemSequential && od.OrderID == orderId);
        }

        public OrdersDetail SearchOrderDetailByOrderIdAndItemSequencial(int orderId, int itemSequencial)
        {
            // Supondo que você tenha uma lista ou um banco de dados de OrdersDetail chamado ordersDetails
            return dbContext.OrdersDetails.FirstOrDefault(od => od.OrderID == orderId && od.ItemSequencial == itemSequencial);
        }

        // Method to search by order ID and return multiple entities
        public IEnumerable<OrdersDetail> SearchOrderDetailById(int orderID)
        {
            return dbContext.OrdersDetails.Where(od => od.OrderID == orderID).ToList();
        }

        // Method to search by book ID and return multiple entities
        public IEnumerable<OrdersDetail> SearchOrderDetailByBookID(int bookID)
        {
            return dbContext.OrdersDetails.Where(od => od.BookID == bookID).ToList();
        }

        public int GetQuantityOpensByBookId(int bookId)
        {
            return dbContext.OrdersDetails
                .Where(od => od.BookID == bookId && od.Order.StatusId == 3)
                .Sum(od => (int?)od.Quantity) ?? 0;
        }

    }
}
