using HiTechDistributionApp.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.BLL.entity
{
    public class OrdersDetailController
    {
        private readonly OrdersDetailRepository ordersDetailRepository;

        public OrdersDetailController()
        {
            ordersDetailRepository = new OrdersDetailRepository();
        }

        public IEnumerable<OrdersDetail> GetOrdersDetails() => ordersDetailRepository.GetOrdersDetails();

        public void AddOrdersDetail(OrdersDetail ordersDetail) => ordersDetailRepository.AddOrdersDetail(ordersDetail);
        public void UpdateOrdersDetail(OrdersDetail ordersDetail) => ordersDetailRepository.UpdateOrdersDetail(ordersDetail);
        public void DeleteOrdersDetail(OrdersDetail ordersDetail) => ordersDetailRepository.DeleteOrdersDetail(ordersDetail);


        //public OrdersDetail SearchOrderDetailById(int orderID) => ordersDetailRepository.SearchOrderDetailById(orderID);
        public OrdersDetail SearchOrderDetailByItemSequencial(int itemSequencial) => ordersDetailRepository.SearchOrderDetailByItemSequencial(itemSequencial);
        //public OrdersDetail SearchOrderDetailByBookID(int bookID) => ordersDetailRepository.SearchOrderDetailByBookID(bookID);

        public OrdersDetail SearchOrderDetailByOrderIdAndItemSequencial(int orderID, int itemSequencial) => ordersDetailRepository.SearchOrderDetailByOrderIdAndItemSequencial(orderID, itemSequencial);
        public IEnumerable<OrdersDetail> SearchOrderDetailById(int orderID) => ordersDetailRepository.SearchOrderDetailById(orderID);
        
        public IEnumerable<OrdersDetail> SearchOrderDetailByBookID(int bookID) => ordersDetailRepository.SearchOrderDetailByBookID(bookID);

        public int SerchOrderDetailOpenByBookId(int bookID) => ordersDetailRepository.GetQuantityOpensByBookId(bookID);
    }
}
