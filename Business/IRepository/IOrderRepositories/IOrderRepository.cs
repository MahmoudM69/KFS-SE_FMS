using Models.DTOModels.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IOrderRepositories
{
    public interface IOrderRepository
    {
        public Task<OrderDTO> CreateOrder(OrderDTO orderDTO);
        public Task<OrderDTO> GetOrder(int Id);
        public Task<ICollection<OrderDTO>> GetAllOrdersAsync();
        public Task<ICollection<OrderDTO>> GetAllCustomerOrders(string id);
        public Task<OrderDTO> UpdateOrder(int id, OrderDTO orderDTO);
        public void DeleteOrder(int Id);
    }
}
