using AutoMapper;
using Business.IRepository.IOrderRepositories;
using DataAcesss.Data;
using DataAcesss.Data.CustomerModels;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.PaymentModels;
using DataAcesss.Data.Shared;
using Microsoft.EntityFrameworkCore;
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

        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void CreateOrder(Order order)
        {
            if (order != null && order.Establishment_ProductId > 0 && order.CustomerId != null)
            {
                var dbOrder = context.Orders.Add(order).Entity;
                if (dbOrder != null)
                {
                    context.SaveChanges();
                }
            }
        }

        public async Task<Order> GetOrder(int id)
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
                    return(order);
                }
            }
            return null;
        }

        public async Task<List<Order>> GetAllCustomerOrders(string id)
        {
            if (id.ToString() != null)
            {
                List<Order> orders = await context.Orders.Include(x => x.Establishment_Product).ThenInclude(y => y.Product)
                                                         .Include(x => x.FinancialAid)
                                                         .Include(x => x.Payment)
                                                         .Where(x => x.CustomerId == id).ToListAsync();
                if(orders != null && orders.Any())
                {
                    return(orders);
                }
            }
            return null;
        }
        public async Task<List<Order>> GetAllEstablishmentOrders(int id)
        {
            if (id.ToString() != null)
            {
                Establishment_Product establishment_Product = await context.Establishment_Products.Include(x => x.Orders).FirstOrDefaultAsync(x => x.EstablishmentId == id);
                if (establishment_Product != null && establishment_Product.Orders.Any())
                {
                    return(establishment_Product.Orders.ToList());
                }
            }
            return null;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            List<Order> orders = await context.Orders.Include(x => x.Customer)
                                                                          .Include(x => x.Establishment_Product).ThenInclude(y => y.Product)
                                                                          .Include(x => x.Establishment_Product).ThenInclude(y => y.Establishment)
                                                                          .Include(x => x.FinancialAid)
                                                                          .Include(x => x.Payment)
                                                                          .Include(x => x.Payment).ToListAsync();
            if (orders.Any())
            {
                return(orders);
            }
            return null;
        }

        public Order UpdateOrder(Order order)
        {
            if (order != null)
            {
                if(order.OrderId > 0 && order != null && order.CustomerId != null && order.Establishment_Product != null)
                {
                    var dbOrder = context.Orders.Update(order);
                    if (dbOrder.Entity != null)
                    {
                        context.SaveChanges();
                        return(dbOrder.Entity);
                    }
                }
            }
            return null;
        }

        public void DeleteOrder(int Id)
        {
            if (Id.ToString() != null)
            {
                Order order = context.Orders.Find(Id);
                order.Payment = null;
                order.Establishment_Product = null;
                order.PaymentId = null;
                order.FinancialAidId = null;
                if (order != null)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
            }
        }
    }
}
