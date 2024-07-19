using HiTechDistributionApp.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiTechDistributionApp.BLL.entity
{
    public class OrderController
    {
        private readonly OrderRepository orderRepository;

        public OrderController() 
        {
            orderRepository = new OrderRepository(); 
        }

        public IEnumerable<Order> GetOrders() => orderRepository.GetOrders();

        public void AddOrder(Order order) => orderRepository.AddOrder(order);

        public void UpdateOrder(Order order) => orderRepository.UpdateOrder(order);

        public void CancelOrder(int orderId) => orderRepository.CancelOrder(orderId);

        public Order GetOrderById(int orderId) => orderRepository.GetOrderById(orderId);
        public IEnumerable<Order> GetOrderByCustomerId(int customerId) => orderRepository.GetOrderByCustomerId(customerId);
        public IEnumerable<Order> GetOrderByEmployeeId(int employeeId) => orderRepository.GetOrderByEmployeeId(employeeId);
        public IEnumerable<Order> GetOrderByStatusId(string statusId) => orderRepository.GetOrdersByState(statusId);

       
    }
}
