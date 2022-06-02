using DataAcesss.Data.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IOrderRepositories
{
    public interface IOrderRepository
    {
        public void CreateOrder(Order order);
        public Task<Order> GetOrder(int Id);
        public Task<List<Order>> GetAllOrdersAsync();
        public Task<List<Order>> GetAllCustomerOrders(string id);
        public Task<List<Order>> GetAllEstablishmentOrders(int id);
        public Order UpdateOrder(Order order);
        public void DeleteOrder(int Id);
    }
}
