using AutoMapper;
using Business.IRepository.IPaymentRepositories;
using DataAcesss.Data;
using DataAcesss.Data.CustomerModels;
using DataAcesss.Data.OrderModels;
using DataAcesss.Data.PaymentModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.PaymentRepositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext context;

        public PaymentRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Payment CreatePayment(Payment payment)
        {
            if (payment != null)
            {
                var dbPayment = context.Payments.Add(payment);
                if (dbPayment != null)
                {
                    context.SaveChanges();
                    return (dbPayment.Entity);
                }
            }
            return null;
        }

        public async Task<Payment> GetPayment(int id)
        {
            if (id.ToString() != null)
            {
                Payment payment = await context.Payments.Include(x => x.PaymentService)
                                                        .Include(x => x.Order).ThenInclude(y => y.Customer)
                                                        .Include(x => x.Order).ThenInclude(y => y.Establishment_Product)
                                                        .Include(x => x.Order).ThenInclude(y => y.FinancialAid)
                                                        .FirstOrDefaultAsync(x => x.PaymentId == id);
                if (payment != null)
                {
                    return (payment);
                }
            }
            return null;
        }

        public async Task<List<Payment>> GetAllCustomerPayments(string id)
        {
            Customer customer = await context.Customers.Include(x => x.Orders).ThenInclude(y => y.Establishment_Product)
                                                            .Include(x => x.Orders).ThenInclude(y => y.FinancialAid)
                                                            .FirstOrDefaultAsync(x => x.Id == id);
            if (customer.Orders.Any())
            {
                List<Payment> payments = new();
                foreach(Order order in customer.Orders)
                {
                    payments.Add(order.Payment);
                }
                return (payments);
            }
            return null;
        }

        public async Task<List<Payment>> GetAllPayments()
        {
            List<Payment> payments = await context.Payments.Include(x => x.PaymentService)
                                                            .Include(x => x.Order).ThenInclude(y => y.Customer)
                                                            .Include(x => x.Order).ThenInclude(y => y.Establishment_Product)
                                                            .Include(x => x.Order).ThenInclude(y => y.FinancialAid)
                                                            .ToListAsync();
            if (payments.Any())
            {
                return (payments);
            }
            return null;
        }

        public async Task<Payment> UpdatePayment(Payment payment)
        {
            if (payment != null)
            {
                if (payment.PaymentId > 0)
                {
                    Payment updatedPayment = context.Payments.Update(payment).Entity;
                    if (updatedPayment != null)
                    {
                        await context.SaveChangesAsync();
                        return (updatedPayment);
                    }
                }
            }
            return null;
        }

        public void DeletePayment(int Id)
        {
            if (Id.ToString() != null)
            {
                Payment payment = context.Payments.Find(Id);
                if (payment != null)
                {
                    context.Payments.Remove(payment);
                    context.SaveChanges();
                }
            }
        }
    }
}