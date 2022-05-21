using AutoMapper;
using Business.IRepository.IOrderRepositories;
using DataAcesss.Data;
using DataAcesss.Data.CustomerModels;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.Shared;
using Microsoft.EntityFrameworkCore;
using Models.DTOModels.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.OrderRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public OrderRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<OrderDTO> CreateOrder(OrderDTO orderDTO)
        {
            if (orderDTO != null && orderDTO.Establishment_ProductDTO != null && orderDTO.CustomerDTO != null)
            {
                Order order = mapper.Map<Order>(orderDTO);
                var dbOrder = await context.Orders.AddAsync(order);
                if (dbOrder.Entity != null)
                {
                    await context.SaveChangesAsync();
                    OrderDTO dbOrderDTO = mapper.Map<OrderDTO>(dbOrder.Entity);
                    return dbOrderDTO;
                }
            }
            return null;
        }

        public async Task<OrderDTO> GetOrder(int id)
        {
            if (id.ToString() != null)
            {
                Order order = await context.Orders.Include(x => x.Establishment_Product).ThenInclude(y => y.Product)
                                                  .Include(x => x.Establishment_Product).ThenInclude(y => y.Establishment)
                                                  .Include(x => x.Customer)
                                                  .Include(x => x.FinancialAid)
                                                  .Include(x => x.Payment)
                                                  .FirstOrDefaultAsync(x => x.OrderId == id);
                if (order != null)
                {
                    return mapper.Map<OrderDTO>(order);
                }
            }
            return null;
        }

        public async Task<ICollection<OrderDTO>> GetAllCustomerOrders(string id)
        {
            if (id.ToString() != null)
            {
                Customer customer = await context.Customers.Include(x => x.Orders).FirstOrDefaultAsync(x => x.Id == id);
                if(customer != null && customer.Orders.Any())
                {
                    ICollection<Order> orders = customer.Orders;
                    return mapper.Map<ICollection<OrderDTO>>(orders);
                }
            }
            return null;
        }

        public async Task<ICollection<OrderDTO>> GetAllOrdersAsync()
        {
            ICollection<Order> orders = await context.Orders.Include(x => x.Customer)
                                                                          .Include(x => x.Establishment_Product).ThenInclude(y => y.Product)
                                                                          .Include(x => x.Establishment_Product).ThenInclude(y => y.Establishment)
                                                                          .Include(x => x.FinancialAid)
                                                                          .Include(x => x.Payment)
                                                                          .Include(x => x.Payment).ToListAsync();
            if (orders.Any())
            {
                return mapper.Map<ICollection<OrderDTO>>(orders);
            }
            return null;
        }

        public async Task<OrderDTO> UpdateOrder(int id, OrderDTO orderDTO)
        {
            if(id.ToString() != null && orderDTO != null && orderDTO.CustomerDTO != null && orderDTO.Establishment_ProductDTO != null)
            {
                Order order = await context.Orders.Include(x => x.Customer)
                                                  .Include(x => x.Establishment_Product)
                                                  .Include(x => x.FinancialAid)
                                                  .Include(x => x.Payment)
                                                  .FirstOrDefaultAsync(x => x.OrderId == id);
                if (order != null)
                {
                    order = mapper.Map<OrderDTO, Order>(orderDTO, order);
                    var dbOrder = context.Orders.Update(order);
                    if (dbOrder.Entity != null)
                    {
                        await context.SaveChangesAsync();
                        return mapper.Map<OrderDTO>(dbOrder.Entity);
                    }
                }
            }
            return null;
        }

        public async void DeleteOrder(int Id)
        {
            if (Id.ToString() != null)
            {
                Order order = await context.Orders.FindAsync(Id);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
