using AutoMapper;
using Business.IRepository.IPaymentRepositories;
using DataAcesss.Data;
using DataAcesss.Data.PaymentModels;
using Microsoft.EntityFrameworkCore;
using Models.DTOModels.OrderDTOs;
using Models.DTOModels.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.PaymentRepositories
{
    internal class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public PaymentRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<PaymentDTO> CreatePayment(PaymentDTO paymentDTO)
        {
            if (paymentDTO != null)
            {
                Payment payment = mapper.Map<Payment>(paymentDTO);
                var dbPayment = await context.Payments.AddAsync(payment);
                if (dbPayment != null)
                {
                    await context.SaveChangesAsync();
                    PaymentDTO dbPaymentDTO = mapper.Map<PaymentDTO>(dbPayment.Entity);
                    return dbPaymentDTO;
                }
            }
            return null;
        }

        public async Task<PaymentDTO> GetPayment(int id)
        {
            if (id.ToString() != null)
            {
                Payment payment = await context.Payments.Include(x => x.PaymentService)
                                                        .FirstOrDefaultAsync(x => x.PaymentId == id);
                if (payment != null)
                {
                    return mapper.Map<PaymentDTO>(payment);
                }
            }
            return null;
        }

        public ICollection<PaymentDTO> GetAllPayments()
        {
            ICollection<Payment> payments = context.Payments.ToList();
            if (payments.Any())
            {
                return mapper.Map<ICollection<PaymentDTO>>(payments);
            }
            return null;
        }

        public async Task<PaymentDTO> UpdatePayment(int id, PaymentDTO paymentDTO)
        {
            if (id.ToString() != null)
            {
                Payment payment = await context.Payments.Include(x => x.Orders)
                                                        .Include(x => x.PaymentService)
                                                        .FirstOrDefaultAsync(x => x.PaymentId == id);
                if (payment != null)
                {
                    if (paymentDTO.OrderDTOs == null)
                        paymentDTO.OrderDTOs = mapper.Map<ICollection<OrderDTO>>(payment.Orders);
                    if(paymentDTO.PaymentServiceDTO == null)
                        paymentDTO.PaymentServiceDTO = mapper.Map<PaymentServiceDTO>(payment.PaymentService);
                    Payment updatedPayment = context.Payments.Update(mapper.Map<PaymentDTO, Payment>(paymentDTO, payment)).Entity;
                    if (updatedPayment != null)
                    {
                        await context.SaveChangesAsync();
                        return mapper.Map<PaymentDTO>(updatedPayment);
                    }
                }
            }
            return null;
        }

        public async void DeletePayment(int Id)
        {
            if (Id.ToString() != null)
            {
                Payment payment = await context.Payments.FindAsync(Id);
                if (payment != null)
                {
                    context.Payments.Remove(payment);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}