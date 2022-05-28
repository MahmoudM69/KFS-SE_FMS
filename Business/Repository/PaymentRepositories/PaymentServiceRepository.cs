using AutoMapper;
using Business.IRepository.IPaymentRepositories;
using DataAcesss.Data;
using DataAcesss.Data.PaymentModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.PaymentRepositories
{
    public class PaymentServiceRepository : IPaymentServiceRepository
    {
        private readonly AppDbContext context;

        public PaymentServiceRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<PaymentService> CreatePaymentService(PaymentService paymentService)
        {
            if(paymentService != null)
            {
                var dbPaymentService = await context.PaymentServices.AddAsync(paymentService);
                if(dbPaymentService != null)
                {
                    await context.SaveChangesAsync();
                    return (dbPaymentService.Entity);
                }
            }
            return null;
        }

        public async Task<PaymentService> GetPaymentService(int id)
        {
            if(id.ToString() != null)
            {
                PaymentService paymentService = await context.PaymentServices.Include(x => x.Payments)
                                                                             .FirstOrDefaultAsync(x => x.PaymentServiceId == id);
                if( paymentService != null)
                {
                    return (paymentService);
                }
            }
            return null;
        }

        public List<PaymentService> GetAllPaymentServices()
        {
            List<PaymentService> paymentServices = context.PaymentServices.ToList();
            if (paymentServices.Any())
            {
                return (paymentServices);
            }
            return null;
        }

        public async Task<PaymentService> UpdatePaymentService(PaymentService paymentService)
        {
            if(paymentService != null)
            {
                if (paymentService.PaymentServiceId > 0 && await context.PaymentServices.FindAsync(paymentService.PaymentServiceId) != null)
                {
                    PaymentService updatedPaymentService = context.PaymentServices.Update(paymentService).Entity;
                    if (updatedPaymentService != null)
                    {
                        await context.SaveChangesAsync();
                        return (updatedPaymentService);
                    }
                }
            }
            return null;
        }

        public async void DeletePaymentService(int Id)
        {
            if(Id.ToString() != null)
            {
                PaymentService paymentService = await context.PaymentServices.FindAsync(Id);
                if(paymentService != null)
                {
                    context.PaymentServices.Remove(paymentService);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
