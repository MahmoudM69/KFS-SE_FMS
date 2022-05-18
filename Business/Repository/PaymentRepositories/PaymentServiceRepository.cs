using AutoMapper;
using Business.IRepository.IPaymentRepositories;
using DataAcesss.Data;
using DataAcesss.Data.PaymentModels;
using Microsoft.EntityFrameworkCore;
using Models.DTOModels.PaymentDTOs;
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
        private readonly IMapper mapper;

        public PaymentServiceRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<PaymentServiceDTO> CreatePaymentService(PaymentServiceDTO paymentServiceDTO)
        {
            if(paymentServiceDTO != null)
            {
                PaymentService paymentService =  mapper.Map<PaymentService>(paymentServiceDTO);
                var dbPaymentService = await context.PaymentServices.AddAsync(paymentService);
                if(dbPaymentService != null)
                {
                    await context.SaveChangesAsync();
                    PaymentServiceDTO dbPaymentServiceDTO = mapper.Map<PaymentServiceDTO>(dbPaymentService.Entity);
                    return dbPaymentServiceDTO;
                }
            }
            return null;
        }

        public async Task<PaymentServiceDTO> GetPaymentService(int id)
        {
            if(id.ToString() != null)
            {
                PaymentService paymentService = await context.PaymentServices.Include(x => x.Payments)
                                                                             .FirstOrDefaultAsync(x => x.PaymentServiceId == id);
                if( paymentService != null)
                {
                    return mapper.Map<PaymentServiceDTO>(paymentService);
                }
            }
            return null;
        }

        public ICollection<PaymentServiceDTO> GetAllPaymentServices()
        {
            ICollection<PaymentService> paymentServices = context.PaymentServices.ToList();
            if (paymentServices.Any())
            {
                return mapper.Map<ICollection<PaymentServiceDTO>>(paymentServices);
            }
            return null;
        }

        public async Task<PaymentServiceDTO> UpdatePaymentService(int id, PaymentServiceDTO paymentServiceDTO)
        {
            if(id.ToString() != null)
            {
                PaymentService paymentService = await context.PaymentServices.Include(x => x.Payments)
                                                                             .FirstOrDefaultAsync(x => x.PaymentServiceId == id);
                if(paymentService != null)
                {
                    if (paymentServiceDTO.PaymentDTOs == null)
                        paymentServiceDTO.PaymentDTOs = mapper.Map<ICollection<PaymentDTO>>(paymentService.Payments);
                    PaymentService updatedPaymentService = context.PaymentServices.Update(mapper.Map<PaymentServiceDTO, PaymentService>(paymentServiceDTO, paymentService)).Entity;
                    if (updatedPaymentService != null)
                    {
                        await context.SaveChangesAsync();
                        return mapper.Map<PaymentServiceDTO>(updatedPaymentService);
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
