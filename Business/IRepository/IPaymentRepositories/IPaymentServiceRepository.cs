using Models.DTOModels.PaymentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IRepository.IPaymentRepositories
{
    public interface IPaymentServiceRepository
    {
        public Task<PaymentServiceDTO> CreatePaymentService(PaymentServiceDTO paymentServiceDTO);
        public Task<PaymentServiceDTO> GetPaymentService(int Id);
        public ICollection<PaymentServiceDTO> GetAllPaymentServices();
        public Task<PaymentServiceDTO> UpdatePaymentService(int id, PaymentServiceDTO paymentServiceDTO);
        public void DeletePaymentService(int Id);
    }
}
